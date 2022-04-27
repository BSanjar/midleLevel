using Aspose.Words;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using midleLevel.models;
using midleLevel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace midleLevel.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IOptions<Settings> _appSettings;
        public HomeController(IOptions<Settings> appSettings)
        {
            _appSettings = appSettings;
        }

        public IActionResult Index(string result = "", string text=null)
        {
            ViewBag.Result = result;
            ViewBag.Text = text;
            return View(GetLanguages());
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile, string fromText, string toText)
        {
            ViewBag.TextResult = null;
            string Result = "";
            string Text = "";
            try
            {
                if (uploadedFile != null && fromText != null && toText != null)
                {
                    Document doc;

                    using (Stream stream = uploadedFile.OpenReadStream())
                    {
                        doc = new Document(stream);
                    }

                    FileParser fp = new FileParser(_appSettings);
                    MethodResult mr = fp.parseFile(doc);

                    if (mr.code == 0)
                    {
                        Translator translator = new Translator(_appSettings);
                        //временно, пока не будет подписки
                        mr.text = mr.text.Replace("Created with an evaluation copy of Aspose.Words. To discover the full versions of our APIs please visit: https://products.aspose.com/words/\r\n\n", "");
                        MethodResult resultTrans = translator.translateText(mr.text, fromText, toText);
                        Result = resultTrans.message;
                        if (resultTrans!=null && resultTrans.code!=null && resultTrans.code == 0)
                        {
                            Result = "success";
                            Text = resultTrans.text;
                        }
                       
                    }
                    else
                    {
                        Result = mr.message;
                    }
                }
                else
                {
                    Result = "invalid input data!";
                }
            }
            catch(Exception ex)
            {
                Result = "Failed to translate the text, please try again";
            }
            return RedirectToAction("Index", new { result = Result, text = Text });
        }

        //временно, можно подвязать к АПИ который возвращает список языков
        public List<Languages> GetLanguages()
        {
            return new List<Languages>()
            {
                    new Languages(){language = "Abkhazian	 ", lnCode = "ab"},
                    new Languages(){language = "Afar	 ", lnCode = "aa"},
                    new Languages(){language = "Afrikaans	 ", lnCode = "af"},
                    new Languages(){language = "Akan	 ", lnCode = "ak"},
                    new Languages(){language = "Albanian	 ", lnCode = "sq"},
                    new Languages(){language = "Amharic	 ", lnCode = "am"},
                    new Languages(){language = "Arabic	 ", lnCode = "ar"},
                    new Languages(){language = "Aragonese	 ", lnCode = "an"},
                    new Languages(){language = "Armenian	 ", lnCode = "hy"},
                    new Languages(){language = "Assamese	 ", lnCode = "as"},
                    new Languages(){language = "Avaric	 ", lnCode = "av"},
                    new Languages(){language = "Avestan	 ", lnCode = "ae"},
                    new Languages(){language = "Aymara	 ", lnCode = "ay"},
                    new Languages(){language = "Azerbaijani	 ", lnCode = "az"},
                    new Languages(){language = "Bambara	 ", lnCode = "bm"},
                    new Languages(){language = "Bashkir	 ", lnCode = "ba"},
                    new Languages(){language = "Basque	 ", lnCode = "eu"},
                    new Languages(){language = "Belarusian	 ", lnCode = "be"},
                    new Languages(){language = "Bengali	 ", lnCode = "bn"},
                    new Languages(){language = "Bislama	 ", lnCode = "bi"},
                    new Languages(){language = "Bosnian	 ", lnCode = "bs"},
                    new Languages(){language = "Breton	 ", lnCode = "br"},
                    new Languages(){language = "Bulgarian	 ", lnCode = "bg"},
                    new Languages(){language = "Burmese	 ", lnCode = "my"},
                    new Languages(){language = "Catalan, Valencian	 ", lnCode = "ca"},
                    new Languages(){language = "Chamorro	 ", lnCode = "ch"},
                    new Languages(){language = "Chechen	 ", lnCode = "ce"},
                    new Languages(){language = "Chichewa, Chewa, Nyanja	 ", lnCode = "ny"},
                    new Languages(){language = "Chinese	 ", lnCode = "zh"},
                    new Languages(){language = "Church Slavic, Old Slavonic, Church Slavonic, Old Bulgarian, Old Church Slavonic	 ", lnCode = "cu"},
                    new Languages(){language = "Chuvash	 ", lnCode = "cv"},
                    new Languages(){language = "Cornish	 ", lnCode = "kw"},
                    new Languages(){language = "Corsican	 ", lnCode = "co"},
                    new Languages(){language = "Cree	 ", lnCode = "cr"},
                    new Languages(){language = "Croatian	 ", lnCode = "hr"},
                    new Languages(){language = "Czech	 ", lnCode = "cs"},
                    new Languages(){language = "Danish	 ", lnCode = "da"},
                    new Languages(){language = "Divehi, Dhivehi, Maldivian	 ", lnCode = "dv"},
                    new Languages(){language = "Dutch, Flemish	 ", lnCode = "nl"},
                    new Languages(){language = "Dzongkha	 ", lnCode = "dz"},
                    new Languages(){language = "English	 ", lnCode = "en"},
                    new Languages(){language = "Esperanto	 ", lnCode = "eo"},
                    new Languages(){language = "Estonian	 ", lnCode = "et"},
                    new Languages(){language = "Ewe	 ", lnCode = "ee"},
                    new Languages(){language = "Faroese	 ", lnCode = "fo"},
                    new Languages(){language = "Fijian	 ", lnCode = "fj"},
                    new Languages(){language = "Finnish	 ", lnCode = "fi"},
                    new Languages(){language = "French	 ", lnCode = "fr"},
                    new Languages(){language = "Western Frisian	 ", lnCode = "fy"},
                    new Languages(){language = "Fulah	 ", lnCode = "ff"},
                    new Languages(){language = "Gaelic, Scottish Gaelic	 ", lnCode = "gd"},
                    new Languages(){language = "Galician	 ", lnCode = "gl"},
                    new Languages(){language = "Ganda	 ", lnCode = "lg"},
                    new Languages(){language = "Georgian	 ", lnCode = "ka"},
                    new Languages(){language = "German	 ", lnCode = "de"},
                    new Languages(){language = "Greek, Modern (1453–)	 ", lnCode = "el"},
                    new Languages(){language = "Kalaallisut, Greenlandic	 ", lnCode = "kl"},
                    new Languages(){language = "Guarani	 ", lnCode = "gn"},
                    new Languages(){language = "Gujarati	 ", lnCode = "gu"},
                    new Languages(){language = "Haitian, Haitian Creole	 ", lnCode = "ht"},
                    new Languages(){language = "Hausa	 ", lnCode = "ha"},
                    new Languages(){language = "Hebrew	 ", lnCode = "he"},
                    new Languages(){language = "Herero	 ", lnCode = "hz"},
                    new Languages(){language = "Hindi	 ", lnCode = "hi"},
                    new Languages(){language = "Hiri Motu	 ", lnCode = "ho"},
                    new Languages(){language = "Hungarian	 ", lnCode = "hu"},
                    new Languages(){language = "Icelandic	 ", lnCode = "is"},
                    new Languages(){language = "Ido	 ", lnCode = "io"},
                    new Languages(){language = "Igbo	 ", lnCode = "ig"},
                    new Languages(){language = "Indonesian	 ", lnCode = "id"},
                    new Languages(){language = "Interlingua (International Auxiliary Language Association)	 ", lnCode = "ia"},
                    new Languages(){language = "Interlingue, Occidental	 ", lnCode = "ie"},
                    new Languages(){language = "Inuktitut	 ", lnCode = "iu"},
                    new Languages(){language = "Inupiaq	 ", lnCode = "ik"},
                    new Languages(){language = "Irish	 ", lnCode = "ga"},
                    new Languages(){language = "Italian	 ", lnCode = "it"},
                    new Languages(){language = "Japanese	 ", lnCode = "ja"},
                    new Languages(){language = "Javanese	 ", lnCode = "jv"},
                    new Languages(){language = "Kannada	 ", lnCode = "kn"},
                    new Languages(){language = "Kanuri	 ", lnCode = "kr"},
                    new Languages(){language = "Kashmiri	 ", lnCode = "ks"},
                    new Languages(){language = "Kazakh	 ", lnCode = "kk"},
                    new Languages(){language = "Central Khmer	 ", lnCode = "km"},
                    new Languages(){language = "Kikuyu, Gikuyu	 ", lnCode = "ki"},
                    new Languages(){language = "Kinyarwanda	 ", lnCode = "rw"},
                    new Languages(){language = "Kirghiz, Kyrgyz	 ", lnCode = "ky"},
                    new Languages(){language = "Komi	 ", lnCode = "kv"},
                    new Languages(){language = "Kongo	 ", lnCode = "kg"},
                    new Languages(){language = "Korean	 ", lnCode = "ko"},
                    new Languages(){language = "Kuanyama, Kwanyama	 ", lnCode = "kj"},
                    new Languages(){language = "Kurdish	 ", lnCode = "ku"},
                    new Languages(){language = "Lao	 ", lnCode = "lo"},
                    new Languages(){language = "Latin	 ", lnCode = "la"},
                    new Languages(){language = "Latvian	 ", lnCode = "lv"},
                    new Languages(){language = "Limburgan, Limburger, Limburgish	 ", lnCode = "li"},
                    new Languages(){language = "Lingala	 ", lnCode = "ln"},
                    new Languages(){language = "Lithuanian	 ", lnCode = "lt"},
                    new Languages(){language = "Luba-Katanga	 ", lnCode = "lu"},
                    new Languages(){language = "Luxembourgish, Letzeburgesch	 ", lnCode = "lb"},
                    new Languages(){language = "Macedonian	 ", lnCode = "mk"},
                    new Languages(){language = "Malagasy	 ", lnCode = "mg"},
                    new Languages(){language = "Malay	 ", lnCode = "ms"},
                    new Languages(){language = "Malayalam	 ", lnCode = "ml"},
                    new Languages(){language = "Maltese	 ", lnCode = "mt"},
                    new Languages(){language = "Manx	 ", lnCode = "gv"},
                    new Languages(){language = "Maori	 ", lnCode = "mi"},
                    new Languages(){language = "Marathi	 ", lnCode = "mr"},
                    new Languages(){language = "Marshallese	 ", lnCode = "mh"},
                    new Languages(){language = "Mongolian	 ", lnCode = "mn"},
                    new Languages(){language = "Nauru	 ", lnCode = "na"},
                    new Languages(){language = "Navajo, Navaho	 ", lnCode = "nv"},
                    new Languages(){language = "North Ndebele	 ", lnCode = "nd"},
                    new Languages(){language = "South Ndebele	 ", lnCode = "nr"},
                    new Languages(){language = "Ndonga	 ", lnCode = "ng"},
                    new Languages(){language = "Nepali	 ", lnCode = "ne"},
                    new Languages(){language = "Norwegian	 ", lnCode = "no"},
                    new Languages(){language = "Norwegian Bokmål	 ", lnCode = "nb"},
                    new Languages(){language = "Norwegian Nynorsk	 ", lnCode = "nn"},
                    new Languages(){language = "Sichuan Yi, Nuosu	 ", lnCode = "ii"},
                    new Languages(){language = "Occitan	 ", lnCode = "oc"},
                    new Languages(){language = "Ojibwa	 ", lnCode = "oj"},
                    new Languages(){language = "Oriya	 ", lnCode = "or"},
                    new Languages(){language = "Oromo	 ", lnCode = "om"},
                    new Languages(){language = "Ossetian, Ossetic	 ", lnCode = "os"},
                    new Languages(){language = "Pali	 ", lnCode = "pi"},
                    new Languages(){language = "Pashto, Pushto	 ", lnCode = "ps"},
                    new Languages(){language = "Persian	 ", lnCode = "fa"},
                    new Languages(){language = "Polish	 ", lnCode = "pl"},
                    new Languages(){language = "Portuguese	 ", lnCode = "pt"},
                    new Languages(){language = "Punjabi, Panjabi	 ", lnCode = "pa"},
                    new Languages(){language = "Quechua	 ", lnCode = "qu"},
                    new Languages(){language = "Romanian, Moldavian, Moldovan	 ", lnCode = "ro"},
                    new Languages(){language = "Romansh	 ", lnCode = "rm"},
                    new Languages(){language = "Rundi	 ", lnCode = "rn"},
                    new Languages(){language = "Russian	 ", lnCode = "ru"},
                    new Languages(){language = "Northern Sami	 ", lnCode = "se"},
                    new Languages(){language = "Samoan	 ", lnCode = "sm"},
                    new Languages(){language = "Sango	 ", lnCode = "sg"},
                    new Languages(){language = "Sanskrit	 ", lnCode = "sa"},
                    new Languages(){language = "Sardinian	 ", lnCode = "sc"},
                    new Languages(){language = "Serbian	 ", lnCode = "sr"},
                    new Languages(){language = "Shona	 ", lnCode = "sn"},
                    new Languages(){language = "Sindhi	 ", lnCode = "sd"},
                    new Languages(){language = "Sinhala, Sinhalese	 ", lnCode = "si"},
                    new Languages(){language = "Slovak	 ", lnCode = "sk"},
                    new Languages(){language = "Slovenian	 ", lnCode = "sl"},
                    new Languages(){language = "Somali	 ", lnCode = "so"},
                    new Languages(){language = "Southern Sotho	 ", lnCode = "st"},
                    new Languages(){language = "Spanish, Castilian	 ", lnCode = "es"},
                    new Languages(){language = "Sundanese	 ", lnCode = "su"},
                    new Languages(){language = "Swahili	 ", lnCode = "sw"},
                    new Languages(){language = "Swati	 ", lnCode = "ss"},
                    new Languages(){language = "Swedish	 ", lnCode = "sv"},
                    new Languages(){language = "Tagalog	 ", lnCode = "tl"},
                    new Languages(){language = "Tahitian	 ", lnCode = "ty"},
                    new Languages(){language = "Tajik	 ", lnCode = "tg"},
                    new Languages(){language = "Tamil	 ", lnCode = "ta"},
                    new Languages(){language = "Tatar	 ", lnCode = "tt"},
                    new Languages(){language = "Telugu	 ", lnCode = "te"},
                    new Languages(){language = "Thai	 ", lnCode = "th"},
                    new Languages(){language = "Tibetan	 ", lnCode = "bo"},
                    new Languages(){language = "Tigrinya	 ", lnCode = "ti"},
                    new Languages(){language = "Tonga (Tonga Islands)	 ", lnCode = "to"},
                    new Languages(){language = "Tsonga	 ", lnCode = "ts"},
                    new Languages(){language = "Tswana	 ", lnCode = "tn"},
                    new Languages(){language = "Turkish	 ", lnCode = "tr"},
                    new Languages(){language = "Turkmen	 ", lnCode = "tk"},
                    new Languages(){language = "Twi	 ", lnCode = "tw"},
                    new Languages(){language = "Uighur, Uyghur	 ", lnCode = "ug"},
                    new Languages(){language = "Ukrainian	 ", lnCode = "uk"},
                    new Languages(){language = "Urdu	 ", lnCode = "ur"},
                    new Languages(){language = "Uzbek	 ", lnCode = "uz"},
                    new Languages(){language = "Venda	 ", lnCode = "ve"},
                    new Languages(){language = "Vietnamese	 ", lnCode = "vi"},
                    new Languages(){language = "Volapük	 ", lnCode = "vo"},
                    new Languages(){language = "Walloon	 ", lnCode = "wa"},
                    new Languages(){language = "Welsh	 ", lnCode = "cy"},
                    new Languages(){language = "Wolof	 ", lnCode = "wo"},
                    new Languages(){language = "Xhosa	 ", lnCode = "xh"},
                    new Languages(){language = "Yiddish	 ", lnCode = "yi"},
                    new Languages(){language = "Yoruba	 ", lnCode = "yo"},
                    new Languages(){language = "Zhuang, Chuang	 ", lnCode = "za"},
                    new Languages(){language = "Zulu	 ", lnCode = "zu"}

            };
        }
    }
}
