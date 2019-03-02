using com.clusterrr.hakchi_gui.Extensions.ModStore;
using com.clusterrr.hakchi_gui.Properties;
using com.clusterrr.hakchi_gui.Tasks;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace com.clusterrr.hakchi_gui.module_library
{
    public class ModStoreManager
    {
        [XmlIgnore]
        [NonSerialized]
        public Form parentForm;
        public List<ModStoreItem> AvailableItems = new List<ModStoreItem>();
        public List<InstalledModItem> InstalledItems = new List<InstalledModItem>();
        public DateTime LastUpdate = new DateTime();
        public bool SortAlphabetically = false;
        public string ConfigPath { get { return Path.Combine(Program.BaseDirectoryExternal, "config", "ModStoreConfig.xml"); } }
        
        public void CheckForDeletedItems()
        {
            string userModDir = Path.Combine(Program.BaseDirectoryExternal, "user_mods");
            for (int i = 0; i < InstalledItems.Count; ++i)
            {
                var item = InstalledItems[i];
                bool removeItem = false;
                if (item.InstalledFiles.Count == 1 && !item.InstalledFiles[0].EndsWith("\\"))
                {
                    removeItem = File.Exists(Path.Combine(userModDir, item.InstalledFiles[0])) == false;
                }
                else
                {
                    foreach (var file in item.InstalledFiles)
                    {
                        if (file.EndsWith("\\") && !Directory.Exists(Path.Combine(userModDir, file)))
                        {
                            removeItem = true;
                            break;
                        }
                    }
                }
                if (removeItem)
                {
                    InstalledItems.RemoveAt(i);
                    --i;
                }
            }
        }

        public void DownloadItem(ModStoreItem item)
        {
            if(item.Type == ModStoreResources.Module)
            {
                DownloadModule(item as Module);
            }
            else if (item.Type == ModStoreResources.Game)
            {
                DownloadGame(item as ModStoreGame);
            }
        }

        private Tasker DownloadFile(string url, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            if (Directory.Exists(fileName))
                Directory.Delete(fileName, true);

            var tasker = new Tasker(parentForm);
            tasker.AttachViews(new Tasks.TaskerTaskbar(), new Tasks.TaskerForm());
            tasker.SetStatusImage(Resources.sign_sync);
            tasker.SetTitle(Resources.DownloadingEllipsis);
            tasker.AddTask(WebClientTasks.DownloadFile(url, fileName));
            return tasker;
        }

        public void DownloadGame(ModStoreGame game)
        {
            try
            {
                var installedGame = GetInstalledModule(game);
                //If game is installed remove it
                if (installedGame != null)
                    InstalledItems.Remove(installedGame);

                string tempFileName = Path.GetTempPath() + game.Path.Substring(game.Path.LastIndexOf("/") + 1);

                if(DownloadFile(game.Path, tempFileName).Start() == Tasker.Conclusion.Success)
                {
                    MainForm.StaticRef.AddGames(new string[] { tempFileName });
                    File.Delete(tempFileName);
                }

                //InstalledItems.Add(game.CreateInstalledItem());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(ModStoreResources.CriticalError, ex.Message + ex.StackTrace), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SaveConfig();
        }

        public void DownloadModule(Module module)
        {
            try
            {
                string userModDir = Path.Combine(Program.BaseDirectoryExternal, "user_mods");
                var installedModule = GetInstalledModule(module);
                //If module is installed remove it
                if (installedModule != null)
                {
                    foreach(var file in installedModule.InstalledFiles)
                    {
                        try
                        {
                            var modPath = Path.Combine(userModDir, file);
                            if (Directory.Exists(modPath))
                                Directory.Delete(modPath, true);

                            if (File.Exists(modPath))
                                File.Delete(modPath);
                        }
                        catch { }
                    }

                    InstalledItems.Remove(installedModule);
                    installedModule = null;
                }
                switch (module.ModType)
                {
                    case ModuleType.hmod:
                        {
                            string fileLocation = Path.Combine(userModDir, module.Path.Substring(module.Path.LastIndexOf('/') + 1));

                            if (DownloadFile(module.Path, fileLocation).Start() == Tasker.Conclusion.Success)
                            {
                                installedModule = module.CreateInstalledItem();
                                installedModule.InstalledFiles.Add(module.Path.Substring(module.Path.LastIndexOf('/') + 1));
                                InstalledItems.Add(installedModule);
                            }
                        }
                        break;
                    case ModuleType.compressedFile:
                        var tempFileName = Path.GetTempFileName();
                        if(DownloadFile(module.Path, tempFileName).Start() == Tasker.Conclusion.Success)
                        {
                            using (var extractor = ArchiveFactory.Open(tempFileName))
                            {
                                installedModule = module.CreateInstalledItem();
                                foreach (var file in extractor.Entries)
                                {
                                    int index = file.Key.IndexOf('/');
                                    if (index != -1)
                                    {
                                        var folder = file.Key.Substring(0, index + 1);
                                        if (!installedModule.InstalledFiles.Contains(folder))
                                        {
                                            installedModule.InstalledFiles.Add(folder);
                                            var localFolder = Path.Combine(userModDir, folder);
                                            if (Directory.Exists(localFolder))
                                                Directory.Delete(localFolder, true);
                                        }
                                    }
                                    else if(!file.IsDirectory)
                                        installedModule.InstalledFiles.Add(file.Key);
                                }
                                extractor.WriteToDirectory(userModDir, new SharpCompress.Common.ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                                InstalledItems.Add(installedModule);
                            }
                            File.Delete(tempFileName);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(ModStoreResources.CriticalError, ex.Message + ex.StackTrace), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SaveConfig();
        }

        public InstalledModItem GetInstalledModule(ModStoreItem repoModule)
        {
            foreach (var module in InstalledItems)
            {
                if (module.Id == repoModule.Id)
                    return module;
            }
            return null;
        }

        public void SaveConfig()
        {
            if (!Directory.Exists(Path.GetDirectoryName(ConfigPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
            XmlSerializer x = new XmlSerializer(typeof(ModStoreManager));
            using (var fs = new FileStream(ConfigPath, FileMode.Create))
            {
                x.Serialize(fs, this);
            }
        }
    }
}
