using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModernSteward;
using System.Speech.Recognition;
using System.Reflection;
using System.IO;

namespace ModernSteward
{
    public class CustomPlugin : PluginFunctionality
    {
        private SlideshowManager slideshow;

		private string PresentationFilePath;

        public override void Trigger(List<KeyValuePair<string, string>> semanticsToDict)
        {
			if (semanticsToDict[0].Key == "power point")
            {
				if (semanticsToDict[1].Key == "next slide")
                {
                    if (null == slideshow)
                    {
                        //TODO: exception handling. Discuss with Liubo about message delivery system.
                    }
                    slideshow.NextSlide();
				}
				if (semanticsToDict[1].Key == "previous slide")
                {
                    if (null == slideshow)
                    {
                        //TODO: see "next slide case"
                    }
                    slideshow.PreviousSlide();
				}
				if (semanticsToDict[1].Key == "close presentation")
                {
                    if (null == slideshow)
                    {
                        //TODO: see "next slide case"
                    }
                    slideshow.ClosePresentation();
				}
				if (semanticsToDict[1].Key == "open presentation")
                {
					if (semanticsToDict[2].Key == "PresentationName")
                    {
						string dictationForPresentationName = semanticsToDict[2].Value;
                        slideshow = new SlideshowManager(dictationForPresentationName);
					}
				}
			}

        }

        public override GrammarBuilder GetGrammarBuilder()
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ModernSteward.CustomPluginGrammar.xml");
                return ModernSteward.TreeViewToGrammarBuilderAlgorithm.CreateGrammarBuilderFromXML(stream);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public override bool Initialize()
        {
			InitializationForm init = new InitializationForm();
			init.Show();
            return true;
        }
    }
}
