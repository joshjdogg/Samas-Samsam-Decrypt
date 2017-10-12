using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Delimon.Win32.IO;

namespace SAMDdec
{
    internal class Program
    {
        #region statics
        private static string salt = "SALT";
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        private static string file_list = ".loveransisgood";//myff11("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", Program.salt);
        private static string special_file_list = myff11("EAAAAORy8/rM976/bGRFgncjWaYTQK7YQDaRoFw7f5HMTg16", Program.salt);
        private static string[] file_list_array = Program.file_list.Split(',');
        private static string[] special_file_list_array = Program.special_file_list.Split(',');
        private static List<List<string>> filesToAttack = new List<List<string>>();
        private static string machine_name_html = Environment.MachineName + "<br><br>";
        private static string current_directory = AppDomain.CurrentDomain.BaseDirectory;
        private static string attack_working_directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + myff11("EAAAAMX+e6YD0ucAvSH9E93+RFtvK+aVDwYy3VUYyszzUDzQ", Program.salt);
        /// <summary>
        /// file loaded from args[0]
        /// </summary>
        private static string external_File_Arg = "";
        private static string help_file_name = myff11("EAAAAF/rebF4vt3ScDLek314X/6TV3DubR1w8tLraPTsMzSntxin+nQmh5yE8ZQfKrx/fg==", Program.salt);
        private static string html_ext = myff11("EAAAAOwMZxCSUQvYGKwF+U67EWoGgDfDQ88FtiBaxpG9Jf5l", Program.salt);
        private static string encrypted_extension = myff11("EAAAAKyFr0jm9ujvS3zm844V84lzH+ti64rS7ZzSaKH2GDE3", Program.salt);
        private static string e14 = "1.7";
        private static string e15 = "12";
        private static string e16 = "6";
        private static string upload_address = myff11("EAAAAC9Paqz8pg34Noozv45YJ8uGgNRq97JhnKvxitBKxW5bscY6Uxxb8yrbB/CRb9R2TPBSq7LRSAdx6OaWFd0uzK8JwzOU8FauPFD5A9PokHn9", Program.salt);
        private static string key_of_some_sort = myff11("EAAAABOIusYqXL6iGskTDcnb/3lpYNMyKSoGIxGa3yAkToiv2k3j3UHo65yH0lCBU+thcp24QNE9PHdPbO9CrzpqME4=", Program.salt);
        private static string help_html = "</html>\r\n<body style='background-color:lightgrey;'>\r\n<pre>\r\n<font color='Red'><center><h3>&#35;&#87;&#104;&#97;&#116;&#32;&#104;&#97;&#112;&#112;&#101;&#110;&#101;&#100;&#32;&#116;&#111;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#63;</h3></center></font>\r\n&#65;&#108;&#108;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#32;&#101;&#110;&#99;&#114;&#121;&#112;&#116;&#101;&#100;&#32;&#119;&#105;&#116;&#104;&#32;&#82;&#83;&#65;&#45;&#50;&#48;&#52;&#56;&#32;&#101;&#110;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#44;&#32;&#70;&#111;&#114;&#32;&#109;&#111;&#114;&#101;&#32;&#105;&#110;&#102;&#111;&#114;&#109;&#97;&#116;&#105;&#111;&#110;&#32;&#115;&#101;&#97;&#114;&#99;&#104;&#32;&#105;&#110;&#32;&#71;&#111;&#111;&#103;&#108;&#101;&#32;&#34;&#82;&#83;&#65;&#32;&#69;&#110;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#34;\r\n<font color='Red'><center><h3>&#35;&#72;&#111;&#119;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#111;&#118;&#101;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#63;</h3></center></font>\r\n&#82;&#83;&#65;&#32;&#105;&#115;&#32;&#97;&#32;&#97;&#115;&#121;&#109;&#109;&#101;&#116;&#114;&#105;&#99;&#32;&#99;&#114;&#121;&#112;&#116;&#111;&#103;&#114;&#97;&#112;&#104;&#105;&#99;&#32;&#97;&#108;&#103;&#111;&#114;&#105;&#116;&#104;&#109;&#44;&#32;&#89;&#111;&#117;&#32;&#110;&#101;&#101;&#100;&#32;&#111;&#110;&#101;&#32;&#107;&#101;&#121;&#32;&#102;&#111;&#114;&#32;&#101;&#110;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#32;&#97;&#110;&#100;&#32;&#111;&#110;&#101;&#32;&#107;&#101;&#121;&#32;&#102;&#111;&#114;&#32;&#100;&#101;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#10;&#83;&#111;&#32;&#121;&#111;&#117;&#32;&#110;&#101;&#101;&#100;&#32;&#80;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#107;&#101;&#121;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#111;&#118;&#101;&#114;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#46;&#10;&#73;&#116;&#39;&#115;&#32;&#110;&#111;&#116;&#32;&#112;&#111;&#115;&#115;&#105;&#98;&#108;&#101;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#111;&#118;&#101;&#114;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#32;&#119;&#105;&#116;&#104;&#111;&#117;&#116;&#32;&#112;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#107;&#101;&#121;\r\n<font color='red'><center><h3>&#35;&#72;&#111;&#119;&#32;&#116;&#111;&#32;&#103;&#101;&#116;&#32;&#112;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#107;&#101;&#121;&#63;</h3></center></font>\r\n&#89;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#103;&#101;&#116;&#32;&#121;&#111;&#117;&#114;&#32;&#112;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#107;&#101;&#121;&#32;&#105;&#110;&#32;&#51;&#32;&#101;&#97;&#115;&#121;&#32;&#115;&#116;&#101;&#112;&#58;<br>\r\n<font color='DrakRed'>&#83;&#116;&#101;&#112;&#49;&#58;</font>&#32;&#89;&#111;&#117;&#32;&#109;&#117;&#115;&#116;&#32;&#115;&#101;&#110;&#100;&#32;&#117;&#115;&#32;<font color='red'>" + Program.e14 + "&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;</font>&#32;&#102;&#111;&#114;&#32;&#101;&#97;&#99;&#104;&#32;&#97;&#102;&#102;&#101;&#99;&#116;&#101;&#100;&#32;&#80;&#67;&#32;&#79;&#82;&#32;<font color='red'>" + Program.e15 + "&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;&#115;</font>&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#101;&#105;&#118;&#101;&#32;&#65;&#76;&#76;&#32;&#80;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#75;&#101;&#121;&#115;&#32;&#102;&#111;&#114;&#32;&#65;&#76;&#76;&#32;&#97;&#102;&#102;&#101;&#99;&#116;&#101;&#100;&#32;&#80;&#67;&#39;&#115;&#46;<br>\r\n<font color='DrakRed'>&#83;&#116;&#101;&#112;&#50;&#58;</font>&#32;&#65;&#102;&#116;&#101;&#114;&#32;&#121;&#111;&#117;&#32;&#115;&#101;&#110;&#100;&#32;&#117;&#115;&#32;<font color='red'>" + Program.e14 + "&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;</font>&#44;&#32;&#76;&#101;&#97;&#118;&#101;&#32;&#97;&#32;&#99;&#111;&#109;&#109;&#101;&#110;&#116;&#32;&#111;&#110;&#32;&#111;&#117;&#114;&#32;&#83;&#105;&#116;&#101;&#32;&#119;&#105;&#116;&#104;&#32;&#116;&#104;&#105;&#115;&#32;&#100;&#101;&#116;&#97;&#105;&#108;&#58;&#32;&#74;&#117;&#115;&#116;&#32;&#119;&#114;&#105;&#116;&#101;&#32;&#89;&#111;&#117;&#114;&#32;&#34;&#72;&#111;&#115;&#116;&#32;&#110;&#97;&#109;&#101;&#34;&#32;&#105;&#110;&#32;&#121;&#111;&#117;&#114;&#32;&#99;&#111;&#109;&#109;&#101;&#110;&#116;<br>\r\n<font color='DrakRed'>*</font>&#89;&#111;&#117;&#114;&#32;&#72;&#111;&#115;&#116;&#32;&#110;&#97;&#109;&#101;&#32;&#105;&#115;&#58;&#32;" + Program.machine_name_html + "<br><br><br><br><br><br><font color='DrakRed'>&#83;&#116;&#101;&#112;&#51;&#58;</font> &#87;&#101;&#32;&#119;&#105;&#108;&#108;&#32;&#114;&#101;&#112;&#108;&#121;&#32;&#116;&#111;&#32;&#121;&#111;&#117;&#114;&#32;&#99;&#111;&#109;&#109;&#101;&#110;&#116;&#32;&#119;&#105;&#116;&#104;&#32;&#97;&#32;&#100;&#101;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#32;&#115;&#111;&#102;&#116;&#119;&#97;&#114;&#101;&#44;&#32;&#89;&#111;&#117;&#32;&#115;&#104;&#111;&#117;&#108;&#100;&#32;&#114;&#117;&#110;&#32;&#105;&#116;&#32;&#111;&#110;&#32;&#121;&#111;&#117;&#114;&#32;&#97;&#102;&#102;&#101;&#99;&#116;&#101;&#100;&#32;&#80;&#67;&#32;&#97;&#110;&#100;&#32;&#97;&#108;&#108;&#32;&#101;&#110;&#99;&#114;&#121;&#112;&#116;&#101;&#100;&#32;&#102;&#105;&#108;&#101;&#115;&#32;&#119;&#105;&#108;&#108;&#32;&#98;&#101;&#32;&#114;&#101;&#99;&#111;&#118;&#101;&#114;&#101;&#100;<br>\r\n<font color='DrakRed'>*</font>&#79;&#117;&#114;&#32;&#83;&#105;&#116;&#101;&#32;&#65;&#100;&#100;&#114;&#101;&#115;&#115;&#58;<a href='" + Program.upload_address + "'><b>" + Program.upload_address + "</b></a><br>\r\n<font color='DrakRed'>*</font>&#79;&#117;&#114;&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;&#32;&#65;&#100;&#100;&#114;&#101;&#115;&#115;&#58;<font color=green><b>" + Program.key_of_some_sort + "</b></font><br>\r\n&#40;&#73;&#102;&#32;&#121;&#111;&#117;&#32;&#115;&#101;&#110;&#100;&#32;&#117;&#115;&#32;<font color='red'>" + Program.e15 + "&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;&#115;</font>&#32;&#70;&#111;&#114;&#32;&#97;&#108;&#108;&#32;&#80;&#67;&#39;&#115;&#44;&#32;&#76;&#101;&#97;&#118;&#101;&#32;&#97;&#32;&#99;&#111;&#109;&#109;&#101;&#110;&#116;&#32;&#111;&#110;&#32;&#111;&#117;&#114;&#32;&#115;&#105;&#116;&#101;&#32;&#119;&#105;&#116;&#104;&#32;&#116;&#104;&#105;&#115;&#32;&#100;&#101;&#116;&#97;&#105;&#108;&#58;&#32;&#74;&#117;&#115;&#116;&#32;&#119;&#114;&#105;&#116;&#101;&#32;&#34;&#70;&#111;&#114;&#32;&#65;&#108;&#108;&#32;&#65;&#102;&#102;&#101;&#99;&#116;&#101;&#100;&#32;&#80;&#67;&#39;&#115;&#34;&#32;&#105;&#110;&#32;&#121;&#111;&#117;&#114;&#32;&#99;&#111;&#109;&#109;&#101;&#110;&#116;&#41;&#10;&#40;&#65;&#108;&#115;&#111;&#32;&#105;&#102;&#32;&#121;&#111;&#117;&#32;&#119;&#97;&#110;&#116;&#32;&#112;&#97;&#121;&#32;&#102;&#111;&#114;&#32;&#34;&#97;&#108;&#108;&#32;&#97;&#102;&#102;&#101;&#99;&#116;&#101;&#100;&#32;&#80;&#67;&#39;&#115;&#34;&#32;&#89;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#112;&#97;&#121;&#32;" + Program.e16 + "&#32;&#66;&#105;&#116;&#99;&#111;&#105;&#110;&#115;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#101;&#105;&#118;&#101;&#32;&#104;&#97;&#108;&#102;&#32;&#111;&#102;&#32;&#107;&#101;&#121;&#115;&#40;&#114;&#97;&#110;&#100;&#111;&#109;&#108;&#121;&#41;&#32;&#97;&#110;&#100;&#32;&#97;&#102;&#116;&#101;&#114;&#32;&#121;&#111;&#117;&#32;&#118;&#101;&#114;&#105;&#102;&#121;&#32;&#105;&#116;&#32;&#115;&#101;&#110;&#100;&#32;&#50;&#110;&#100;&#32;&#104;&#97;&#108;&#102;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#101;&#105;&#118;&#101;&#32;&#97;&#108;&#108;&#32;&#107;&#101;&#121;&#115;&#32;&#41;<br> \r\n<font color='red'><center><h3>&#72;&#111;&#119;&#32;&#84;&#111;&#32;&#65;&#99;&#99;&#101;&#115;&#115;&#32;&#84;&#111;&#32;&#79;&#117;&#114;&#32;&#83;&#105;&#116;&#101;</h3></center></font>\r\n&#70;&#111;&#114;&#32;&#97;&#99;&#99;&#101;&#115;&#115;&#32;&#116;&#111;&#32;&#111;&#117;&#114;&#32;&#115;&#105;&#116;&#101;&#32;&#121;&#111;&#117;&#32;&#109;&#117;&#115;&#116;&#32;&#105;&#110;&#115;&#116;&#97;&#108;&#108;&#32;&#84;&#111;&#114;&#32;&#98;&#114;&#111;&#119;&#115;&#101;&#114;&#32;&#97;&#110;&#100;&#32;&#101;&#110;&#116;&#101;&#114;&#32;&#111;&#117;&#114;&#32;&#115;&#105;&#116;&#101;&#32;&#85;&#82;&#76;&#32;&#105;&#110;&#32;&#121;&#111;&#117;&#114;&#32;&#116;&#111;&#114;&#32;&#98;&#114;&#111;&#119;&#115;&#101;&#114;&#46;&#10;&#89;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#100;&#111;&#119;&#110;&#108;&#111;&#97;&#100;&#32;&#116;&#111;&#114;&#32;&#98;&#114;&#111;&#119;&#115;&#101;&#114;&#32;&#102;&#114;&#111;&#109;&#32;<font color='DrakRed'></font><a href='https://www.torproject.org/download/download.html.en'>https://www.torproject.org/download/download.html.en</a>\r\n&#70;&#111;&#114;&#32;&#109;&#111;&#114;&#101;&#32;&#105;&#110;&#102;&#111;&#114;&#109;&#97;&#116;&#105;&#111;&#110;&#32;&#112;&#108;&#101;&#97;&#115;&#101;&#32;&#115;&#101;&#97;&#114;&#99;&#104;&#32;&#105;&#110;&#32;&#71;&#111;&#111;&#103;&#108;&#101;&#32;&#34;&#72;&#111;&#119;&#32;&#116;&#111;&#32;&#97;&#99;&#99;&#101;&#115;&#115;&#32;&#111;&#110;&#105;&#111;&#110;&#32;&#115;&#105;&#116;&#101;&#115;&#34;&#32;<br><br>\r\n<font color='red'><center><h3>&#35;&#32;&#84;&#101;&#115;&#116;&#32;&#68;&#101;&#99;&#114;&#121;&#112;&#116;&#105;&#111;&#110;&#32;&#35;</h3></center></font><br>\r\n&#67;&#104;&#101;&#99;&#107;&#32;&#111;&#117;&#114;&#32;&#115;&#105;&#116;&#101;&#44;&#32;&#89;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#117;&#112;&#108;&#111;&#97;&#100;&#32;&#50;&#32;&#101;&#110;&#99;&#114;&#121;&#112;&#116;&#101;&#100;&#32;&#102;&#105;&#108;&#101;&#115;&#32;&#97;&#110;&#100;&#32;&#119;&#101;&#32;&#119;&#105;&#108;&#108;&#32;&#100;&#101;&#99;&#114;&#121;&#112;&#116;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;&#32;&#97;&#115;&#32;&#100;&#101;&#109;&#111;&#46;&#32;&#32;<br><h3><b>&#73;&#102;&#32;&#121;&#111;&#117;&#32;&#97;&#114;&#101;&#32;&#119;&#111;&#114;&#114;&#121;&#32;&#116;&#104;&#97;&#116;&#32;&#121;&#111;&#117;&#32;&#100;&#111;&#110;&#39;&#116;&#32;&#103;&#101;&#116;&#32;&#121;&#111;&#117;&#114;&#32;&#107;&#101;&#121;&#115;&#32;&#97;&#102;&#116;&#101;&#114;&#32;&#121;&#111;&#117;&#32;&#112;&#97;&#105;&#100;&#44;&#32;&#89;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#103;&#101;&#116;&#32;&#111;&#110;&#101;&#32;&#107;&#101;&#121;&#32;&#102;&#111;&#114;&#32;&#102;&#114;&#101;&#101;&#32;&#111;&#110;&#32;&#121;&#111;&#117;&#32;&#99;&#104;&#111;&#105;&#115;&#101;&#40;&#101;&#120;&#99;&#101;&#112;&#116;&#32;&#105;&#109;&#112;&#111;&#114;&#116;&#97;&#110;&#116;&#32;&#115;&#101;&#114;&#118;&#101;&#114;&#115;&#41;&#44;&#32;&#84;&#101;&#108;&#108;&#32;&#117;&#115;&#32;&#111;&#110;&#101;&#32;&#111;&#102;&#32;&#121;&#111;&#117;&#114;&#32;&#104;&#111;&#115;&#116;&#110;&#97;&#109;&#101;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#101;&#105;&#118;&#101;&#32;&#116;&#104;&#101;&#32;&#102;&#114;&#101;&#101;&#32;&#107;&#101;&#121;&#10;&#65;&#108;&#115;&#111;&#32;&#121;&#111;&#117;&#32;&#99;&#97;&#110;&#32;&#103;&#101;&#116;&#32;&#115;&#111;&#109;&#101;&#32;&#115;&#105;&#110;&#103;&#108;&#101;&#32;&#107;&#101;&#121;&#32;&#97;&#110;&#100;&#32;&#105;&#102;&#32;&#97;&#108;&#108;&#32;&#115;&#105;&#110;&#103;&#108;&#101;&#32;&#66;&#84;&#67;&#32;&#116;&#97;&#104;&#116;&#32;&#121;&#111;&#117;&#32;&#112;&#97;&#105;&#100;&#32;&#114;&#101;&#97;&#99;&#104;&#101;&#100;&#32;&#116;&#111;&#32;&#97;&#108;&#108;&#32;&#107;&#101;&#121;&#115;&#32;&#112;&#114;&#105;&#99;&#101;&#32;&#121;&#111;&#117;&#32;&#119;&#105;&#108;&#108;&#32;&#103;&#101;&#116;&#32;&#97;&#108;&#108;&#32;&#107;&#101;&#121;&#115;&#10;&#65;&#110;&#121;&#119;&#97;&#121;&#32;&#98;&#101;&#32;&#115;&#117;&#114;&#101;&#32;&#116;&#104;&#97;&#116;&#32;&#121;&#111;&#117;&#32;&#119;&#105;&#108;&#108;&#32;&#103;&#101;&#116;&#32;&#97;&#108;&#108;&#32;&#121;&#111;&#117;&#114;&#32;&#107;&#101;&#121;&#115;&#32;&#105;&#102;&#32;&#121;&#111;&#117;&#32;&#112;&#97;&#105;&#100;&#32;&#102;&#111;&#114;&#32;&#116;&#104;&#101;&#109;&#32;&#97;&#110;&#100;&#32;&#119;&#101;&#32;&#100;&#111;&#110;&#39;&#116;&#32;&#119;&#97;&#110;&#116;&#32;&#100;&#97;&#109;&#97;&#103;&#101;&#32;&#111;&#117;&#114;&#32;&#114;&#101;&#108;&#105;&#97;&#98;&#105;&#108;&#105;&#116;&#121;&#10;&#87;&#105;&#116;&#104;&#32;&#98;&#117;&#121;&#105;&#110;&#103;&#32;&#116;&#104;&#101;&#32;&#102;&#105;&#114;&#115;&#116;&#32;&#107;&#101;&#121;&#32;&#121;&#111;&#117;&#32;&#119;&#105;&#108;&#108;&#32;&#102;&#105;&#110;&#100;&#32;&#116;&#104;&#97;&#116;&#32;&#119;&#101;&#32;&#97;&#114;&#101;&#32;&#104;&#111;&#110;&#101;&#115;&#116;&#46;</b></h3><br>\r\n<font color='red'><center><h3>&#35;&#87;&#104;&#101;&#114;&#101;&#32;&#116;&#111;&#32;&#98;&#117;&#121;&#32;&#66;&#105;&#116;&#99;&#111;&#105;&#110;</h3></center></font><br>\r\n&#87;&#101;&#32;&#97;&#100;&#118;&#105;&#99;&#101;&#32;&#121;&#111;&#117;&#32;&#116;&#111;&#32;&#98;&#117;&#121;&#32;&#66;&#105;&#116;&#99;&#111;&#105;&#110;&#32;&#119;&#105;&#116;&#104;&#32;&#67;&#97;&#115;&#104;&#32;&#68;&#101;&#112;&#111;&#115;&#105;&#116;&#32;&#111;&#114;&#32;&#87;&#101;&#115;&#116;&#101;&#114;&#110;&#85;&#110;&#105;&#111;&#110;&#32;&#70;&#114;&#111;&#109;&#32;&#104;&#116;&#116;&#112;&#115;&#58;&#47;&#47;&#108;&#111;&#99;&#97;&#108;&#98;&#105;&#116;&#99;&#111;&#105;&#110;&#115;&#46;&#99;&#111;&#109;&#47;&#32;&#111;&#114;&#32;&#104;&#116;&#116;&#112;&#115;&#58;&#47;&#47;&#99;&#111;&#105;&#110;&#99;&#97;&#102;&#101;&#46;&#99;&#111;&#109;&#47;&#98;&#117;&#121;&#98;&#105;&#116;&#99;&#111;&#105;&#110;&#115;&#119;&#101;&#115;&#116;&#101;&#114;&#110;&#46;&#112;&#104;&#112;&#10;&#66;&#101;&#99;&#97;&#117;&#115;&#101;&#32;&#116;&#104;&#101;&#121;&#32;&#100;&#111;&#110;&#39;&#116;&#32;&#110;&#101;&#101;&#100;&#32;&#97;&#110;&#121;&#32;&#118;&#101;&#114;&#105;&#102;&#105;&#99;&#97;&#116;&#105;&#111;&#110;&#32;&#97;&#110;&#100;&#32;&#115;&#101;&#110;&#100;&#32;&#121;&#111;&#117;&#114;&#32;&#66;&#105;&#116;&#99;&#111;&#105;&#110;&#32;&#113;&#117;&#105;&#99;&#107;&#108;&#121;&#46;<br><br>\r\n<font color='red'><center><h3>&#35;&#100;&#101;&#97;&#100;&#108;&#105;&#110;&#101;</h3></center></font><br>\r\n&#89;&#111;&#117;&#32;&#106;&#117;&#115;&#116;&#32;&#104;&#97;&#118;&#101;&#32;&#55;&#32;&#100;&#97;&#121;&#115;&#32;&#116;&#111;&#32;&#115;&#101;&#110;&#100;&#32;&#117;&#115;&#32;&#116;&#104;&#101;&#32;&#66;&#105;&#116;&#67;&#111;&#105;&#110;&#32;&#97;&#102;&#116;&#101;&#114;&#32;&#55;&#32;&#100;&#97;&#121;&#115;&#32;&#119;&#101;&#32;&#119;&#105;&#108;&#108;&#32;&#114;&#101;&#109;&#111;&#118;&#101;&#32;&#121;&#111;&#117;&#114;&#32;&#112;&#114;&#105;&#118;&#97;&#116;&#101;&#32;&#107;&#101;&#121;&#115;&#32;&#97;&#110;&#100;&#32;&#105;&#116;&#39;&#115;&#32;&#105;&#109;&#112;&#111;&#115;&#115;&#105;&#98;&#108;&#101;&#32;&#116;&#111;&#32;&#114;&#101;&#99;&#111;&#118;&#101;&#114;&#32;&#121;&#111;&#117;&#114;&#32;&#102;&#105;&#108;&#101;&#115;\r\n</pre></html>";
        private static string system_root_drive = Path.GetPathRoot(Environment.SystemDirectory);
        private static string this_process_name = Process.GetCurrentProcess().ProcessName + ".exe";
        private static string bat_file_contents = myff11("EAAAAPy5bjfoSQSjVPm262UhQL7c0IHqksbXo2BnPALCs5F+kmc60HFMnaFzNMal4SimQNr5BpPNTJQEQqTdkyuoFcQ=", Program.salt) + Program.this_process_name + myff11("EAAAAPu+3cFZ5AV5eOuBabb8u5UGW0KqqhLUDpd+bLhufLA1", Program.salt) + Program.current_directory + myff11("EAAAAJhCsEFE8KJqj0T+607J+4JNVE+KOh/1Z6QDpTnWPmYSScA2t+aIcT81wWYCMv14TsYf+BsZzm2e1QxE2+YatGkpzceJjurG14pL8Slelrjg28YN9YWW62YrhMp4rUSbqp/XtLb4eEIBBjqtDiyM7d3AGn62PC0eR81o3ebfPr7PcKItoUagUz5Qx+KqEF0XZ/5bCm3/p2YyChLc9vYwniDXCYHzyFdOAeZdsIcHYJGzigW4cMz0rVBCRRfR3wwtvTP7CUA/9g8iTcEbT0gVhtM=", Program.salt);
        private static string bat_file_name = myff11("EAAAAEL1xd77lIVe9C0TG8HkleKPlT05VmhARyY4WBboM1DF", Program.salt);
        private static string start_info_bat_path = Program.attack_working_directory + "\\" + Program.bat_file_name;
        private static string windows_path = myff11("EAAAAAkeh5APALVqxq/UYwyAcFT1zhr8IA+mF/ROFma4UEWG", Program.salt);
        private static string winnt_path = myff11("EAAAAPuN69xn3UGzk43lgHcHms2aEFiI1xghoF5qApH1VBIx", Program.salt);
        private static string ref_assemblies_path = myff11("EAAAALjLXuiBxifH2aSTXCLvmUDAxFM6UUGgre9TPDi0ZfRtlYSRYyh0lEFfSWKlOlEEag==", Program.salt);
        private static string recycle_path = myff11("EAAAAP7TWgNdexpV2NYmVa82TXfQ2wkwdkHg91UcVARIkR6N", Program.salt);
        private static string users_all_users_path = myff11("EAAAAF0L4p1vlYBuLHKrW95diBqYZvPddnyjStRlaDP0UH8y", Program.salt);
        private static string doc_settings_all_users_path = myff11("EAAAAB0NcVk0d+vT6j6RrOSdJxglDHF1kDojiDrd1ygF3gWF/4BRKViEuA/d7JlmIokXTcHzR7BA1jl3Njca5hSn01U=", Program.salt);
        private static string boot_path = myff11("EAAAAJQViS7iXteGq8mV5PqLOng0Ex0OFeIU9NglszmhOw6M", Program.salt);
        private static string users_default_path = myff11("EAAAAFr56FHZzgjUceFq4/lLST9QsLAnOZ0qz0VZV1/QK8bt", Program.salt);
        #endregion
        private static string[] ext_enc;

        private static string helpfile;

        private static string helpfileext;

        private static string wi_ndo_ws_d_r_iv_e_;

        private static List<string> mylist;

        private static string selfname;

        private static string privkey;

        private static List<string> bad_dec;
        private static bool test;

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
            Program.test = false;
        }

        public Program()
        {
        }

        public static void dec(List<string> ll)
        {
            Console.WriteLine(string.Concat("\r\n[+] ", ll.Count, " Files Found."));
            Thread.Sleep(3000);
            long totalSize = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int i = 0; i < ll.Count; i++)
            Parallel.For(0, ll.Count - 1, i =>
            {
                try
                {
                    //Console.WriteLine($"Iteration {i}: {ll[i]}");
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
                Interlocked.Increment(ref totalSize);

                Program.ShowPercentProgress("[+] Progress: ", totalSize, ll.Count);
            });

            stopwatch.Stop();
            Console.WriteLine("loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        public static void dec2(List<string> ll)
        {
            Console.WriteLine(string.Concat("\r\n[+] ", ll.Count, " Files Found."));
            Thread.Sleep(3000);
            long totalSize = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int i = 0; i < ll.Count; i++)
            Parallel.For(0, ll.Count - 1, i =>
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

                Interlocked.Increment(ref totalSize);

                Program.ShowPercentProgress("[+] Progress: ", totalSize, ll.Count);
            });

            stopwatch.Stop();
            Console.WriteLine("loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        public static void decryptFile(string encryptedFilePath)
        {
            string str = Program.MakePath(encryptedFilePath, "");
            try
            {
                string innerText = "";
                string innerText1 = "";
                long num = (long) 0;
                string stringFromBytes = Encipher.GetStringFromBytes(Encipher.GetHeaderBytesFromFile(encryptedFilePath));
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(stringFromBytes);
                foreach (object elementsByTagName in xmlDocument.GetElementsByTagName("AAA"))
                {
                    innerText = ((XmlNode) elementsByTagName).InnerText;
                }
                foreach (object obj in xmlDocument.GetElementsByTagName("AA"))
                {
                    innerText1 = ((XmlNode) obj).InnerText;
                }
                foreach (object elementsByTagName1 in xmlDocument.GetElementsByTagName("AAAAAAAAAAAAAAAAAA"))
                {
                    num = Convert.ToInt64(((XmlNode) elementsByTagName1).InnerText);
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
            for (int i = 0; i < (int) directories.Length; i++)
            {
                string str = directories[i];
                if (Directory.Exists(string.Concat(str, "\\Desktop")))
                {
                    try
                    {
                        FileInfo[] files = (new DirectoryInfo(string.Concat(str, "\\Desktop"))).GetFiles();
                        for (int j = 0; j < (int) files.Length; j++)
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
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            for (int i = 0; i < (int) drives.Length; i++)
            {
                System.IO.DriveInfo driveInfo = drives[i];
                try
                {
                    if (driveInfo.IsReady)
                    {
                        Program.recursivegetfiles(driveInfo.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error driveinfo {driveInfo.Name} - {ex}");
                }
            }
            if (!test)
            {
                if (Program.mylist.Count > 0)
                {
                    Program.dec(Program.mylist);
                    return;
                }
                Console.WriteLine("[+] No Affected Files.");
            }
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
            using (new ConsoleLogger($"cleanup.{DateTimeOffset.Now.Ticks}.log"))
            {
                if ((int) args.Length < 1)
                {
                    //Console.WriteLine(string.Concat("\r\n[+] Usage:\r\n\t", Program.selfname, " private.keyxml\r\n"));
                    //return;
                    args = new string[] { current_directory };
                }
#if !DEBUG
                // EnsureRunningAdmin();
#endif
                if ((int) args.Length == 1)
                {
                    Console.WriteLine("\r\n====================================================================");
                    Console.WriteLine("\r\n[+] Please be Patient, It May Take Several Minutes or Hours");
                    Console.WriteLine("[+] Searching For Affected Files.\r\n[+] Please Wait.");
                    Thread.Sleep(3000);
                    if (args[0] == "-test")
                    {
                        Program.test = true;
                            Program.go_to_dec();
                            Program.mylist.Clear();
                    }
                    else
                    {
                        try
                        {
                            var key = EnsureMachineKeyExists(args[0]);
                            Program.privkey = File.ReadAllText(key);
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
                }
                if ((int) args.Length == 3)
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
                        for (int i = 0; i < (int) files.Length; i++)
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
                                    innerText = ((XmlNode) elementsByTagName).InnerText;
                                }
                                foreach (object elementsByTagName1 in xmlDocument.GetElementsByTagName("AA"))
                                {
                                    innerText1 = ((XmlNode) elementsByTagName1).InnerText;
                                }
                                foreach (object elementsByTagName2 in xmlDocument.GetElementsByTagName("AAAAAAAAAAAAAAAAAA"))
                                {
                                    Convert.ToInt64(((XmlNode) elementsByTagName2).InnerText);
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
        }


        private static string EnsureMachineKeyExists(string path)
        {
            Console.WriteLine($"Checking machine key: {path}");
            //if starts with a . expand out to full path. File lib cant handle relative pathing :(
            if (path.StartsWith("."))
            {

                if (path.StartsWith(@".\"))
                    path = path.Remove(0, 2);
                if (path.StartsWith("."))
                    path = path.Remove(0, 1);
                path = Path.Combine(current_directory, path);
            }

            if (File.Exists(path))
            {
                // does it end in keyxml?
                if (path.EndsWith("keyxml"))
                    return path;

            }
            //try to see if its relative
            if (File.Exists(Path.Combine(current_directory, path)))
            {
                var testpath = Path.Combine(current_directory, path);
                //are there any keys here
                var files = Directory.GetFiles(testpath, "*.keyxml", SearchOption.TopDirectoryOnly);
                if (files.Any())
                    path = testpath;
                else
                {
                    testpath = Path.Combine(testpath, "privkey");
                    if (File.Exists(testpath))
                    {
                        files = Directory.GetFiles(testpath, "*.keyxml", SearchOption.TopDirectoryOnly);
                        if (files.Any())
                            path = testpath;

                    }
                    //try subfolder
                }
            }
            var keyPath = path;
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "*.keyxml", SearchOption.TopDirectoryOnly);
                if (!files.Any())
                {
                    var testpath = Path.Combine(path, "privkey");
                    files = Directory.GetFiles(testpath, "*.keyxml", SearchOption.TopDirectoryOnly);
                    if (files.Any())
                        keyPath = testpath;

                }
                //resolve dir name the same way
                // assumes co-location.
            }

            if (Directory.Exists(keyPath))
            {
                // derive the machine key
                var machineKeyInfo = GetMachineKeyInfo(keyPath);
                if (!machineKeyInfo.Exists)
                {
                    Console.WriteLine($"Failed to locate machine key '{machineKeyInfo.FullName}'");
                    Environment.Exit(1);
                }
                return machineKeyInfo.FullName;
            }
            else
            {
                Console.WriteLine($"Failed to locate machine key");
                Environment.Exit(1);
                return "";
            }

        }

        private static FileInfo GetMachineKeyInfo(string keyPath)
        {
            var machine = Environment.MachineName;
            Console.WriteLine($"MachineName: {machine}");

            if (machine.EndsWith("tritech.local"))
                machine = machine.Replace("tritech.local", string.Empty);
            // full path to the private key            
            var machineKeyFileName = $"{machine}_PrivateKey.keyxml";
            var fullPath = Path.Combine(keyPath, machineKeyFileName);
            Console.WriteLine($"looking for key {machineKeyFileName}");
            try
            {
                return new FileInfo(fullPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot access file info for '{fullPath}'\n{e}");
            }

            return new FileInfo("FILENOTFOUND");
        }
        private static void EnsureRunningAdmin()
        {
            Console.WriteLine("Checking privileges");
            var isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            if (!isAdmin)
            {
                Console.Write("The application must be run as an administrator.");
                Environment.Exit(1);
            }
        }
        private static string MakePath(string plainFilePath, string newSuffix)
        {
            string str = string.Concat(Path.GetFileNameWithoutExtension(plainFilePath), newSuffix);
            return Path.Combine(Path.GetDirectoryName(plainFilePath), str);
        }

        public static void myddeecc(string pathfile)
        {
            if (Program.isValidFilePath(pathfile) && (new FileInfo(pathfile)).Length > (long) 0)
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
            if (Program.isValidFilePath(pathfile) && (new FileInfo(pathfile)).Length > (long) 0)
            {
                Program.decryptFile(pathfile);
            }
        }

        public static void recursivegetfilesorig(string path)
        {
            int i;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles();
            for (i = 0; i < (int) files.Length; i++)
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
            for (i = 0; i < (int) directories.Length; i++)
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
        public static void recursivegetfiles(string path)
        {
            Console.WriteLine($"looking for files: *{Program.ext_enc[0]}");
            var locallist = Search2(path, $"*{Program.ext_enc[0]}").Select(f => {
                if (test) Console.WriteLine($"File: {f}");
                Program.mylist.Add(f);
                return f;
            }).ToList();
            //Search(path, $"*{Program.ext_enc[0]}");
            var infectedDirectories = Program.mylist.Select(x => Path.GetDirectoryName(x)).Distinct().Select(f => {
                if (test) Console.WriteLine($"Directory: {f}");
                return f;
            }).ToList();
            Console.WriteLine($"Found {locallist.Count} infected files in {infectedDirectories.Count} infected directories.");
            //if (!infectedDirectories.Any())
            //    return;
            //Console.Write($"Print Files and Directories? (y/n): ");
            //var answer = Console.ReadLine();
            //if (answer.Trim().ToLower() == "y")
            //{
            //    Program.mylist.Sort();
            //    foreach (var file in Program.mylist)
            //    {
            //        Console.WriteLine($"File: {file}");
            //    }
            //    infectedDirectories.Sort();
            //    foreach (var file in infectedDirectories)
            //    {
            //        Console.WriteLine($"Directory: {file}");
            //    }
            //}
            //Console.Write($"Repair? (y/n): ");
            //answer = Console.ReadLine();
            //if (answer.Trim().ToLower() != "y")
            //{
            //    Program.mylist = new List<string>();
            //}

        }

        private static void ShowPercentProgress(string message, long currElementIndex, long totalElementCount)
        {
            if (currElementIndex < 0 || currElementIndex >= totalElementCount)
            {
                throw new InvalidOperationException("currElement out of range");
            }
            currElementIndex++;
            double num = (double) (currElementIndex * 100) / Convert.ToDouble(totalElementCount);
            Console.Error.Write("\r{0}{1} %", message, num);
            if (currElementIndex == totalElementCount - 1)
            {
                Console.WriteLine(Environment.NewLine);
            }
        }

        /// <summary>
        ///     Search for files in the path that match a pattern.
        ///     The search includes all sub-directories as well.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static void Search(string path, string searchPattern)
        {

            if (!(path.ToLower() != Program.system_root_drive.ToLower() + Program.windows_path) || !(path.ToLower() != Program.system_root_drive.ToLower() + Program.winnt_path) || (path.ToLower().Contains(Program.ref_assemblies_path) || path.ToLower().Contains(Program.recycle_path)) || (path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.users_all_users_path.ToLower()) || path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.doc_settings_all_users_path.ToLower()) || (path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.boot_path) || path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.users_default_path))))
               return;
            if (!checkdir(path))
                return;
            //if (JunctionPoint.Exists(path))
            //    return;
            DirectoryInfo directoryInfo1 = new DirectoryInfo(path);
            foreach (FileInfo fileInfo in directoryInfo1.GetFiles())
            {
                bool yield = false;
                try
                {
                    if (Program.ShouldAttackFile(fileInfo.FullName))
                    {
                        string ext = Path.GetExtension(fileInfo.FullName);
                        long length = fileInfo.Length;
                        if (Array.Exists<string>(Program.file_list_array, (Predicate<string>) (element => element == ext.ToLower())))
                        {
                            mylist.Add(fileInfo.FullName);
                            //yield = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error searching file:{fileInfo.FullName}  ex => {ex.Message}");
                }
                //if (yield)
                //    yield return fileInfo.FullName;
            }
            foreach (DirectoryInfo directoryInfo2 in directoryInfo1.GetDirectories())
            {
                if ((directoryInfo2.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                {
                   // Console.WriteLine($"JP: {directoryInfo2.FullName}");
                    continue;
                }
                bool yield = false;
                IEnumerable<string> results = Enumerable.Empty<string>();
                try
                {

                        Program.Search(directoryInfo2.FullName, searchPattern);
                    yield = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error searching dir:{directoryInfo2.FullName}  ex => {ex.Message}");

                }
                //if(yield)
                //    foreach (var file in results)
                //    {
                //        yield return file;
                //    }
            }
        }


        private static bool checkdir(string path)
        {
            bool exists = false;
            try
            {
                if (Directory.Exists(path))
                    exists = true;
                else
                    exists = false;
            }
            catch(Exception ex)
            {
                exists = false;
                Console.WriteLine($"error exists: {path} ex: {ex.Message}");
            }
            return exists;
        }

        public static bool ShouldAttackFile(string path_for_check)
        {
            try
            {
                string str90 = Path.GetExtension(path_for_check).ToLower();
                FileInfo fileInfo = new FileInfo(path_for_check);
                return !(str90.ToLower() != Program.encrypted_extension.ToLower())
                    && !path_for_check.ToLower().Contains(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).ToLower())
                    && (!path_for_check.Contains(Program.system_root_drive) || !path_for_check.ToLower().Contains("microsoft\\windows") && !path_for_check.ToLower().Contains("appdata") && (!(str90 == ".ini") && !(str90 == ".sys") && !(str90 == ".dll")))
                    && (!(Directory.GetParent(path_for_check).FullName.ToLower() == Program.system_root_drive.ToLower()) || !File.Exists(fileInfo.FullName));
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<string> Search2(string root, string searchPattern)
        {
            var pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;

                if (!(path.ToLower() != Program.system_root_drive.ToLower() + Program.windows_path) || !(path.ToLower() != Program.system_root_drive.ToLower() + Program.winnt_path) || (path.ToLower().Contains(Program.ref_assemblies_path) || path.ToLower().Contains(Program.recycle_path)) || (path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.users_all_users_path.ToLower()) || path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.doc_settings_all_users_path.ToLower()) || (path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.boot_path) || path.ToLower().Contains(Program.system_root_drive.ToLower() + Program.users_default_path))))
                    continue;
                if (!checkdir(path))
                    continue;
                DirectoryInfo directoryInfo1 = new DirectoryInfo(path);
                if ((directoryInfo1.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                {
                    //Console.WriteLine($"JP: {directoryInfo1.FullName}");
                    continue;
                }
                if (path.ToLower().Contains(@"appdata\local\microsoft\windows\inetcache\low\content.ie5"))
                    continue;
                try
                {
                    next = Directory.GetFiles(path, searchPattern);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error GetFiles {path} - {ex.Message}");
                }
                if (next != null && next.Length != 0)
                    foreach (var file in next) yield return file;
                try
                {

                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch (Exception ex) { Console.WriteLine($"error GetDirectories {path} - {ex.Message}"); }
            }
        }


        public static string myff11(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");
            RijndaelManaged rijndaelManaged1 = (RijndaelManaged) null;
            try
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Program._salt);
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    rijndaelManaged1 = new RijndaelManaged();
                    rijndaelManaged1.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged1.KeySize / 8);
                    rijndaelManaged1.IV = ReadByteArray((System.IO.Stream) memoryStream);
                    RijndaelManaged rijndaelManaged2 = rijndaelManaged1;
                    byte[] key = rijndaelManaged2.Key;
                    byte[] iv = rijndaelManaged1.IV;
                    ICryptoTransform decryptor = rijndaelManaged2.CreateDecryptor(key, iv);
                    using (CryptoStream cryptoStream = new CryptoStream((System.IO.Stream) memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader streamReader = new System.IO.StreamReader((System.IO.Stream) cryptoStream))
                            return streamReader.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (rijndaelManaged1 != null)
                    rijndaelManaged1.Clear();
            }
        }
        private static byte[] ReadByteArray(System.IO.Stream s)
        {
            byte[] buffer1 = new byte[4];
            if (s.Read(buffer1, 0, buffer1.Length) != buffer1.Length)
                throw new SystemException("Stream did not contain properly formatted byte array");
            byte[] buffer2 = new byte[BitConverter.ToInt32(buffer1, 0)];
            if (s.Read(buffer2, 0, buffer2.Length) != buffer2.Length)
                throw new SystemException("Did not read byte array properly");
            return buffer2;
        }
    }

}