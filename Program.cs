using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;

namespace SAMDdec
{
	internal class Program
	{
		private static string[] ext_enc;

		private static string helpfile;

		private static string helpfileext;

		private static string wi_ndo_ws_d_r_iv_e_;

		private static List<string> mylist;

		private static string selfname;

		private static string privkey;

		private static List<string> bad_dec;

		static Program()
		{
			Program.ext_enc = new string[] { ".loveransisgood" };
			Program.helpfile = "DO-YOU-WANT-FILES";
			Program.helpfileext = ".html";
			Program.wi_ndo_ws_d_r_iv_e_ = Path.GetPathRoot(Environment.SystemDirectory);
			Program.mylist = new List<string>();
			Program.selfname = string.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
			Program.privkey = "";
			Program.bad_dec = new List<string>();
		}

		public Program()
		{
		}

		public static void dec(List<string> ll)
		{
			Console.WriteLine(string.Concat("\r\n[+] ", ll.Count, " Files Found."));
			Thread.Sleep(3000);
			for (int i = 0; i < ll.Count; i++)
			{
				try
				{
					Program.myddeecc(ll[i]);
					if (File.Exists(ll[i]))
					{
						FileInfo fileInfo = new FileInfo(ll[i]);
						if (File.Exists(string.Concat(new object[] { fileInfo.Directory, "\\", Program.helpfile, Program.helpfileext })))
						{
							File.Delete(string.Concat(new object[] { fileInfo.Directory, "\\", Program.helpfile, Program.helpfileext }));
						}
						for (int j = 0; j < 10; j++)
						{
							if (File.Exists(string.Concat(new object[] { fileInfo.Directory, "\\00", j.ToString(), "-", Program.helpfile, Program.helpfileext })))
							{
								File.Delete(string.Concat(new object[] { fileInfo.Directory, "\\00", j.ToString(), "-", Program.helpfile, Program.helpfileext }));
							}
						}
					}
				}
				catch
				{
				}
				Program.ShowPercentProgress("[+] Progress: ", i, ll.Count);
			}
		}

		public static void dec2(List<string> ll)
		{
			Console.WriteLine(string.Concat("\r\n[+] ", ll.Count, " Files Found."));
			Thread.Sleep(3000);
			for (int i = 0; i < ll.Count; i++)
			{
				try
				{
					Program.myddeecc2(ll[i]);
					if (File.Exists(ll[i]))
					{
						FileInfo fileInfo = new FileInfo(ll[i]);
						if (File.Exists(string.Concat(new object[] { fileInfo.Directory, "\\", Program.helpfile, Program.helpfileext })))
						{
							File.Delete(string.Concat(new object[] { fileInfo.Directory, "\\", Program.helpfile, Program.helpfileext }));
						}
						for (int j = 0; j < 10; j++)
						{
							if (File.Exists(string.Concat(new object[] { fileInfo.Directory, "\\00", j.ToString(), "-", Program.helpfile, Program.helpfileext })))
							{
								File.Delete(string.Concat(new object[] { fileInfo.Directory, "\\00", j.ToString(), "-", Program.helpfile, Program.helpfileext }));
							}
						}
					}
				}
				catch
				{
				}
				Program.ShowPercentProgress("[+] Progress: ", i, ll.Count);
			}
		}

		public static void decryptFile(string encryptedFilePath)
		{
			string str = Program.MakePath(encryptedFilePath, "");
			try
			{
				string innerText = "";
				string innerText1 = "";
				long num = (long)0;
				string stringFromBytes = Encipher.GetStringFromBytes(Encipher.GetHeaderBytesFromFile(encryptedFilePath));
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(stringFromBytes);
				foreach (object elementsByTagName in xmlDocument.GetElementsByTagName("AAA"))
				{
					innerText = ((XmlNode)elementsByTagName).InnerText;
				}
				foreach (object obj in xmlDocument.GetElementsByTagName("AA"))
				{
					innerText1 = ((XmlNode)obj).InnerText;
				}
				foreach (object elementsByTagName1 in xmlDocument.GetElementsByTagName("AAAAAAAAAAAAAAAAAA"))
				{
					num = Convert.ToInt64(((XmlNode)elementsByTagName1).InnerText);
				}
				byte[] numArray = Encipher.RSADescryptBytes(Convert.FromBase64String(innerText), Program.privkey);
				byte[] numArray1 = Encipher.RSADescryptBytes(Convert.FromBase64String(innerText1), Program.privkey);
				Encipher.DecryptFile(encryptedFilePath, str, numArray, numArray1, num);
			}
			catch (FormatException formatException1)
			{
				FormatException formatException = formatException1;
				Console.WriteLine(string.Concat("\r\n[-] Decryption key is not correct -> ", encryptedFilePath, formatException.Message));
				if (File.Exists(str))
				{
					File.Delete(str);
				}
			}
			catch (XmlException xmlException1)
			{
				XmlException xmlException = xmlException1;
				Console.WriteLine(string.Concat("\r\n[-] Encrypted data is not correct -> ", encryptedFilePath, xmlException.Message));
				if (File.Exists(str))
				{
					File.Delete(str);
				}
			}
		}

		public static void delete_desktop_helps()
		{
			string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.GetEnvironmentVariable("userprofile")).FullName);
			for (int i = 0; i < (int)directories.Length; i++)
			{
				string str = directories[i];
				if (Directory.Exists(string.Concat(str, "\\Desktop")))
				{
					try
					{
						FileInfo[] files = (new DirectoryInfo(string.Concat(str, "\\Desktop"))).GetFiles();
						for (int j = 0; j < (int)files.Length; j++)
						{
							FileInfo fileInfo = files[j];
							if (fileInfo.Name.Contains(Program.helpfile))
							{
								File.Delete(fileInfo.FullName);
							}
						}
					}
					catch (Exception exception)
					{
					}
				}
			}
		}

		public static void go_to_dec()
		{
			Program.mylist.Clear();
			DriveInfo[] drives = DriveInfo.GetDrives();
			for (int i = 0; i < (int)drives.Length; i++)
			{
				DriveInfo driveInfo = drives[i];
				try
				{
					if (driveInfo.IsReady)
					{
						Program.recursivegetfiles(driveInfo.Name);
					}
				}
				catch
				{
				}
			}
			if (Program.mylist.Count > 0)
			{
				Program.dec(Program.mylist);
				return;
			}
			Console.WriteLine("[+] No Affected Files.");
		}

		public static bool isValidFilePath(string strFilePath)
		{
			bool flag = false;
			try
			{
				if (File.Exists(strFilePath))
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
			}
			return flag;
		}

		private static void Main(string[] args)
		{
			if ((int)args.Length < 1)
			{
				Console.WriteLine(string.Concat("\r\n[+] Usage:\r\n\t", Program.selfname, " private.keyxml\r\n"));
				return;
			}
			if ((int)args.Length == 1)
			{
				Console.WriteLine("\r\n====================================================================");
				Console.WriteLine("\r\n[+] Please be Patient, It May Take Several Minutes or Hours");
				Console.WriteLine("[+] Searching For Affected Files.\r\n[+] Please Wait.");
				Thread.Sleep(3000);
				try
				{
					Program.privkey = File.ReadAllText(args[0]);
					Program.go_to_dec();
					Console.WriteLine("\r\n====================================================================\r\nTry");
					Program.dec2(Program.bad_dec);
					Program.mylist.Clear();
					Program.delete_desktop_helps();
					Console.WriteLine("\r\n[+] All File Decrypted.");
					Thread.Sleep(3000);
				}
				catch
				{
				}
			}
			if ((int)args.Length == 3)
			{
				if (args[0] == "-f")
				{
					Program.privkey = File.ReadAllText(args[1]);
					Program.recursivegetfiles(args[2]);
					if (Program.mylist.Count > 0)
					{
						try
						{
							Program.dec(Program.mylist);
							Program.dec(Program.mylist);
							Program.dec(Program.mylist);
							Program.mylist.Clear();
							Console.WriteLine("\r\n[+] All File Decrypted.");
							Thread.Sleep(3000);
						}
						catch
						{
						}
					}
				}
				if (args[0] == "-k")
				{
					string[] files = Directory.GetFiles(args[2]);
					for (int i = 0; i < (int)files.Length; i++)
					{
						string str = files[i];
						Program.privkey = File.ReadAllText(str);
						try
						{
							string innerText = "";
							string innerText1 = "";
							string stringFromBytes = Encipher.GetStringFromBytes(Encipher.GetHeaderBytesFromFile(args[1]));
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.LoadXml(stringFromBytes);
							foreach (object elementsByTagName in xmlDocument.GetElementsByTagName("AAA"))
							{
								innerText = ((XmlNode)elementsByTagName).InnerText;
							}
							foreach (object elementsByTagName1 in xmlDocument.GetElementsByTagName("AA"))
							{
								innerText1 = ((XmlNode)elementsByTagName1).InnerText;
							}
							foreach (object elementsByTagName2 in xmlDocument.GetElementsByTagName("AAAAAAAAAAAAAAAAAA"))
							{
								Convert.ToInt64(((XmlNode)elementsByTagName2).InnerText);
							}
							byte[] numArray = Encipher.RSADescryptBytes(Convert.FromBase64String(innerText), Program.privkey);
							Encipher.RSADescryptBytes(Convert.FromBase64String(innerText1), Program.privkey);
							if (numArray.Length != 0)
							{
								Console.WriteLine(string.Concat("\r\nCORRECT KEY IS:", str));
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		private static string MakePath(string plainFilePath, string newSuffix)
		{
			string str = string.Concat(Path.GetFileNameWithoutExtension(plainFilePath), newSuffix);
			return Path.Combine(Path.GetDirectoryName(plainFilePath), str);
		}

		public static void myddeecc(string pathfile)
		{
			if (Program.isValidFilePath(pathfile) && (new FileInfo(pathfile)).Length > (long)0)
			{
				Program.decryptFile(pathfile);
				if (!File.Exists(pathfile.Replace(Program.ext_enc[0], "")))
				{
					Program.bad_dec.Add(pathfile);
				}
			}
		}

		public static void myddeecc2(string pathfile)
		{
			if (Program.isValidFilePath(pathfile) && (new FileInfo(pathfile)).Length > (long)0)
			{
				Program.decryptFile(pathfile);
			}
		}

		public static void recursivegetfiles(string path)
		{
			int i;
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileInfo[] files = directoryInfo.GetFiles();
			for (i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					string extension = Path.GetExtension(fileInfo.FullName);
					if (Array.Exists<string>(Program.ext_enc, (string element) => element == extension))
					{
						Program.mylist.Add(fileInfo.FullName);
					}
				}
				catch (UnauthorizedAccessException unauthorizedAccessException)
				{
				}
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (i = 0; i < (int)directories.Length; i++)
			{
				DirectoryInfo directoryInfo1 = directories[i];
				try
				{
					Program.recursivegetfiles(directoryInfo1.FullName);
				}
				catch (UnauthorizedAccessException unauthorizedAccessException1)
				{
				}
			}
		}

		private static void ShowPercentProgress(string message, int currElementIndex, int totalElementCount)
		{
			if (currElementIndex < 0 || currElementIndex >= totalElementCount)
			{
				throw new InvalidOperationException("currElement out of range");
			}
			currElementIndex++;
			double num = (double)(currElementIndex * 100) / Convert.ToDouble(totalElementCount);
			Console.Write("\r{0}{1} %", message, num);
			if (currElementIndex == totalElementCount - 1)
			{
				Console.WriteLine(Environment.NewLine);
			}
		}
	}
}