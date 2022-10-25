using System.Diagnostics;
using System.Windows.Forms;

namespace KingdomFileReplacer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public enum FileMode { PAC, SOUND, DOL, STAGEBASE, ANY };

		private Dictionary<string, string> Selected_Folder;

		private void InstructionButton_Click(object sender, EventArgs e)
		{
			// I formatted the text like that so it is grouped by step
			// idk how to do it better because C++ brain
			MessageBox.Show
				(
				"Step 1: Select a copy of Dokapon Kingdom.\n" +
				"Step 2: Select what to replace.\n" +
				"Step 3: Select a folder with the necessary files.\n" +
				"Step 4: Apply the modification.\n\n" +
				"Only ISO and WBFS can be modified.\n" +
				"This program will automatically handle all file extraction and replacement.\n" +
				"If you select Any, this program will extract and search all the game files and replace a file if it can find it. As a result, Any is slower than the other options.\n" +
				"When selecting Any, files located in the GAME.PAC will not be replaced if a new GAME.PAC is provided separately.\n" +
				"This program assumes all file names match what is being replaced.\n" +
				"After pressing Apply, a new ISO file will be created in the same folder as the original game file. This will overwrite an existing file if it has the same name as the output. The new ISO can then be run through standard means.",
				"Instructions and Notes",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
				);
		}

		private void SelectFolderButton_Click(object sender, EventArgs e)
		{
			// set the label and folder path
			SelectedFolderLabel.Text = "Folder:";
			if (SelectFolderDialog.ShowDialog() == DialogResult.OK)
			{
				// the files are put in a dictionary so the paths are all cached,
				// as well as to allow for specific files to be gotten and for iteration over all files if necessary
				Selected_Folder = new Dictionary<string, string>();
				string[] file_names = Directory.GetFiles(SelectFolderDialog.SelectedPath);
				for (int i = 0; i < file_names.Length; i++)
				{
					Selected_Folder[Path.GetFileName(file_names[i])] = file_names[i];
				}
				// the full path isnt shown because it gets cut off by the small window
				SelectedFolderLabel.Text = $"Folder: {Path.GetFileName(SelectFolderDialog.SelectedPath)}";
			}
		}

		private void SelectGameFileButton_Click(object sender, EventArgs e)
		{
			// set the label and file path
			SelectedGameFileLabel.Text = "File:";
			if (SelectGameFileDialog.ShowDialog() == DialogResult.OK)
			{
				// the full path isnt shown because it gets cut off by the small window
				SelectedGameFileLabel.Text = $"File: {Path.GetFileName(SelectGameFileDialog.FileName)}";
			}
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			// check if everything was selected
			if (Selected_Folder == null || SelectFolderDialog.SelectedPath == null || SelectGameFileDialog.FileName == null || ModeComboBox.SelectedIndex == -1)
			{
				MessageBox.Show
					(
					"The required game file, folder directory, or replacement mode was not selected.",
					"Error: Unselected Fields",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
				return;
			}

			// apply file replacement in the way selected
			switch ((FileMode)ModeComboBox.SelectedIndex)
			{
				case FileMode.PAC:
					{
						// Check if GAME.PAC and GAME.PAH exist
						string pac_path;
						string pah_path;
						if (!Selected_Folder.TryGetValue("GAME.PAC", out pac_path) || !Selected_Folder.TryGetValue("GAME.PAH", out pah_path))
						{
							MessageBox.Show
								(
								"GAME.PAC or GAME.PAH was not found in the selected directory.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// extract files with WIT
						Process? extract = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"extract -o \"{SelectGameFileDialog.FileName}\" \"GameFiles\"",
						});
						extract?.WaitForExit();
						extract?.Close();

						// verify extraction
						if (!Directory.Exists("GameFiles"))
						{
							MessageBox.Show
								(
								"Game files directory not found after extraction attempt.",
								"Error: Extraction Failed",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get path to directory containing GAME.PAC and GAME.PAH
						string files_folder_path;
						if (Directory.Exists(Path.Combine("GameFiles", "DATA", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "DATA", "files");
						}
						else if (Directory.Exists(Path.Combine("GameFiles", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "files");
						}
						else
						{
							MessageBox.Show
								(
								"The expected directory containing the GAME.PAC and GAME.PAH was not found.",
								"Error: Directory not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// verify the pac and pah files exist
						if (!File.Exists(Path.Combine(files_folder_path, "GAME.PAC")) || !File.Exists(Path.Combine(files_folder_path, "GAME.PAH")))
						{
							MessageBox.Show
								(
								"The GAME.PAC or GAME.PAH file was not found in the extracted game files.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// copy in pac and pah
						File.Copy(pac_path, Path.Combine(files_folder_path, "GAME.PAC"), true);
						File.Copy(pah_path, Path.Combine(files_folder_path, "GAME.PAH"), true);

						// create new iso
						Process? process = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"copy -o -R \"GameFiles\" \"{Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")}\"",
						});
						process?.WaitForExit();
						process?.Close();

						// check if iso was made
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")))
						{
							MessageBox.Show
								(
								"The new ISO was not found in the expected directory.",
								"Error: ISO not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}
						else
						{
							MessageBox.Show
								(
								"Replacement was successful.",
								"Success",
								MessageBoxButtons.OK
								);
						}
						break;
					}
				case FileMode.SOUND:
					{
						// start WIT
						Process? extract = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"extract -o \"{SelectGameFileDialog.FileName}\" \"GameFiles\"",
						});
						extract?.WaitForExit();
						extract?.Close();

						// verify extraction
						if (!Directory.Exists("GameFiles"))
						{
							MessageBox.Show
								(
								"Game files directory not found after extraction attempt.",
								"Error: Extraction Failed",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get path to directory containing sound files
						string files_folder_path;
						if (Directory.Exists(Path.Combine("GameFiles", "DATA", "files", "sound")))
						{
							files_folder_path = Path.Combine("GameFiles", "DATA", "files", "sound");
						}
						else if (Directory.Exists(Path.Combine("GameFiles", "files", "sound")))
						{
							files_folder_path = Path.Combine("GameFiles", "files", "sound");
						}
						else
						{
							MessageBox.Show
								(
								"The expected directory containing the sound files was not found.",
								"Error: Directory not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get an array of all of the sound files
						string[] sound_files = Directory.GetFiles(files_folder_path);

						// replace files in sound directory if they match the name of a file in the selected folder
						int file_count = 0;
						foreach (var sound_file in sound_files)
						{
							string input_path;
							if (Selected_Folder.TryGetValue(Path.GetFileName(sound_file), out input_path))
							{
								File.Copy(input_path, sound_file, true);
								file_count++;
							}
						}

						// create new iso
						Process? process = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"copy -o \"GameFiles\" \"{Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")}\"",
						});
						process?.WaitForExit();
						process?.Close();

						// check if iso was made
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")))
						{
							MessageBox.Show
								(
								"The new ISO was not found in the expected directory.",
								"Error: ISO not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}
						else
						{
							MessageBox.Show
								(
								$"Replacement was successful. {file_count} sound files were replaced.",
								"Success",
								MessageBoxButtons.OK
								);
						}

						break;
					}
				case FileMode.DOL:
					{
						// Check if main.dol exists
						string dol_path;
						if (!Selected_Folder.TryGetValue("main.dol", out dol_path))
						{
							MessageBox.Show
								(
								"main.dol was not found in the selected directory.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// extract files with WIT
						Process? extract = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"extract -o \"{SelectGameFileDialog.FileName}\" \"GameFiles\"",
						});
						extract?.WaitForExit();
						extract?.Close();

						// verify extraction
						if (!Directory.Exists("GameFiles"))
						{
							MessageBox.Show
								(
								"Game files directory not found after extraction attempt.",
								"Error: Extraction Failed",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get path to directory containing the main.dol
						string files_folder_path;
						if (Directory.Exists(Path.Combine("GameFiles", "DATA", "sys")))
						{
							files_folder_path = Path.Combine("GameFiles", "DATA", "sys");
						}
						else if (Directory.Exists(Path.Combine("GameFiles", "sys")))
						{
							files_folder_path = Path.Combine("GameFiles", "sys");
						}
						else
						{
							MessageBox.Show
								(
								"The expected directory containing the main.dol was not found.",
								"Error: Directory not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// verify the dol file exists
						if (!File.Exists(Path.Combine(files_folder_path, "main.dol")))
						{
							MessageBox.Show
								(
								"The main.dol was not found in the extracted game files.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// copy new dol into the game files
						File.Copy(dol_path, Path.Combine(files_folder_path, "main.dol"), true);

						// create new iso
						Process? process = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"copy -o \"GameFiles\" \"{Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")}\"",
						});
						process?.WaitForExit();
						process?.Close();

						// check if iso was made
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")))
						{
							MessageBox.Show
								(
								"The new ISO was not found in the expected directory.",
								"Error: ISO not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}
						else
						{
							MessageBox.Show
								(
								"Replacement was successful.",
								"Success",
								MessageBoxButtons.OK
								);
						}

						break;
					}
				case FileMode.STAGEBASE:
					{
						// Check if STAGEBASE.DAT exists
						string stagebase_path;
						if (!Selected_Folder.TryGetValue("STAGEBASE.DAT", out stagebase_path))
						{
							MessageBox.Show
								(
								"STAGEBASE.DAT was not found in the selected directory.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// extract files with WIT
						Process? extract = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"extract -o \"{SelectGameFileDialog.FileName}\" \"GameFiles\"",
						});
						extract?.WaitForExit();
						extract?.Close();

						// verify extraction
						if (!Directory.Exists("GameFiles"))
						{
							MessageBox.Show
								(
								"Game files directory not found after extraction attempt.",
								"Error: Extraction Failed",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get path to directory containing GAME.PAC and GAME.PAH
						string files_folder_path;
						if (Directory.Exists(Path.Combine("GameFiles", "DATA", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "DATA", "files");
						}
						else if (Directory.Exists(Path.Combine("GameFiles", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "files");
						}
						else
						{
							MessageBox.Show
								(
								"The expected directory containing the GAME.PAC and GAME.PAH was not found.",
								"Error: Directory not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Extract files from GAME.PAC
						if (!PACManager.PAC.Unpack(Path.Combine(files_folder_path, "GAME.PAC"), Path.Combine(files_folder_path, "GAME.PAH")))
						{
							MessageBox.Show
								(
								"GAME.PAC extraction failed",
								"Error: PAC could not be extracted",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// verify the stagebase file exists
						if (!File.Exists(Path.Combine("PACFiles", "STAGEBASE.DAT")))
						{
							MessageBox.Show
								(
								"The STAGEBASE.DAT was not found in the extracted game files.",
								"Error: File not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// copy stagebase into pacfiles
						File.Copy(stagebase_path, Path.Combine("PACFiles", "STAGEBASE.DAT"), true);

						// Repack GAME.PAC
						PACManager.PAC.Pack(Path.Combine(files_folder_path));

						// create new iso
						Process? process = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"copy -o \"GameFiles\" \"{Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")}\"",
						});
						process?.WaitForExit();
						process?.Close();

						// check if iso was made
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")))
						{
							MessageBox.Show
								(
								"The new ISO was not found in the expected directory.",
								"Error: ISO not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}
						else
						{
							MessageBox.Show
								(
								"Replacement was successful.",
								"Success",
								MessageBoxButtons.OK
								);
						}

						break;
					}
				case FileMode.ANY:
					{
						// extract files with WIT
						Process? extract = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"extract -o \"{SelectGameFileDialog.FileName}\" \"GameFiles\"",
						});
						extract?.WaitForExit();
						extract?.Close();

						// verify extraction
						if (!Directory.Exists("GameFiles"))
						{
							MessageBox.Show
								(
								"Game files directory not found after extraction attempt.",
								"Error: Extraction Failed",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Get path to directory containing GAME.PAC and GAME.PAH
						string files_folder_path;
						if (Directory.Exists(Path.Combine("GameFiles", "DATA", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "DATA", "files");
						}
						else if (Directory.Exists(Path.Combine("GameFiles", "files")))
						{
							files_folder_path = Path.Combine("GameFiles", "files");
						}
						else
						{
							MessageBox.Show
								(
								"The expected directory containing the GAME.PAC and GAME.PAH was not found.",
								"Error: Directory not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// Extract files from GAME.PAC
						if (!PACManager.PAC.Unpack(Path.Combine(files_folder_path, "GAME.PAC"), Path.Combine(files_folder_path, "GAME.PAH")))
						{
							MessageBox.Show
								(
								"GAME.PAC extraction failed",
								"Error: PAC could not be extracted",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}

						// create arrays with every file in both the GameFiles and PACFiles
						string[] game_files = Directory.GetFiles("GameFiles", "*.*", SearchOption.AllDirectories);
						string[] pac_files = Directory.GetFiles("PACFiles", "*.*", SearchOption.AllDirectories);

						// for every file in each array, check if it matches a file in the selected folder
						// if it does, replace it with the file in the selected folder
						int file_count = 0;
						bool repack = true;
						foreach (var game_file in game_files)
						{
							string input_path;
							if (Selected_Folder.TryGetValue(Path.GetFileName(game_file), out input_path))
							{
								File.Copy(input_path, game_file, true);
								if (Path.GetFileName(game_file) == "GAME.PAC" || Path.GetFileName(game_file) == "GAME.PAH")
									repack = false;
								file_count++;
							}
						}
						if (repack)
						{
							foreach (var pac_file in pac_files)
							{
								string input_path;
								if (Selected_Folder.TryGetValue(Path.GetFileName(pac_file), out input_path))
								{
									File.Copy(input_path, pac_file, true);
									file_count++;
								}
							}

							// Repack GAME.PAC
							PACManager.PAC.Pack(Path.Combine(files_folder_path));
						}

						// create new iso
						Process? process = Process.Start(new ProcessStartInfo()
						{
							FileName = Path.Combine("WIT", "wit.exe"),
							Arguments = $"copy -o \"GameFiles\" \"{Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")}\"",
						});
						process?.WaitForExit();
						process?.Close();

						// check if iso was made
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(SelectGameFileDialog.FileName), $"DokaponKingdom-Modified{ModeComboBox.SelectedItem}.iso")))
						{
							MessageBox.Show
								(
								"The new ISO was not found in the expected directory.",
								"Error: ISO not found",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
							return;
						}
						else
						{
							MessageBox.Show
								(
								$"Replacement was successful. {file_count} files were replaced.",
								"Success",
								MessageBoxButtons.OK
								);
						}

						break;
					}
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Delete extracted files
			if (Directory.Exists("GameFiles"))
				Directory.Delete("GameFiles", true);
			if (Directory.Exists("PACFiles"))
				Directory.Delete("PACFiles", true);
			if (File.Exists("order.txt"))
				File.Delete("order.txt");
		}
	}
}