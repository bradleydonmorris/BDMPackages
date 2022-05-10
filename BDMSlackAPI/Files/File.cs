using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMSlackAPI.Files
{
    public class File
    {

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        public Int32 Created { get; set; }

        [JsonProperty("timestamp")]
        public String Timestamp { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        [JsonProperty("mimetype")]
        public String MimeType { get; set; }

        [JsonProperty("filetype")]
        public String FileType { get; set; }

        [JsonProperty("pretty_type")]
        public String PrettyType { get; set; }

        [JsonProperty("user")]
        public String User { get; set; }

        [JsonProperty("editable")]
        public Boolean Editable { get; set; }

        [JsonProperty("size")]
        public Int64 Size { get; set; }

        /*
           "id": "F0TD00400",
                "created": 1532293501,
                "timestamp": 1532293501,
                "name": "dramacat.gif",
                "title": "dramacat",
                "mimetype": "image/jpeg",
                "filetype": "gif",
                "pretty_type": "JPEG",
                "user": "U0L4B9NSU",
                "editable": false,
                "size": 43518,
                "mode": "hosted",
                "is_external": false,
                "external_type": "",
                "is_public": false,
                "public_url_shared": false,
                "display_as_bot": false,
                "username": "",
                "url_private": "https://.../dramacat.gif",
                "url_private_download": "https://.../dramacat.gif",
                "thumb_64": "https://.../dramacat_64.gif",
                "thumb_80": "https://.../dramacat_80.gif",
                "thumb_360": "https://.../dramacat_360.gif",
                "thumb_360_w": 360,
                "thumb_360_h": 250,
                "thumb_480": "https://.../dramacat_480.gif",
                "thumb_480_w": 480,
                "thumb_480_h": 334,
                "thumb_160": "https://.../dramacat_160.gif",
                "image_exif_rotation": 1,
                "original_w": 526,
                "original_h": 366,
                "permalink": "https://.../dramacat.gif",
                "permalink_public": "https://.../More-Path-Components",
                "comments_count": 0,
                "is_starred": false, 

         */
    }
}
