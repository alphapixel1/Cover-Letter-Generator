using Cover_Letter_Generator.Templates;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.Util;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.Template.Templates
{
    internal class BasicTemplate : ITemplate
    {
        public string Name => "Basic Template";

        public XWPFDocument ApplyTemplate(UserInfo info,string recipient, string Text)
        {
            XWPFDocument doc = new XWPFDocument();

            //margins
            CT_SectPr sectPr = doc.Document.body.AddNewSectPr();
            CT_PageMar pageMar = sectPr.AddPageMar();
            pageMar.left=720L;
            pageMar.top=720L;
            pageMar.right=720L;
            pageMar.bottom = 1440L;


            XWPFHeader head=doc.CreateHeaderFooterPolicy().CreateHeader(ST_HdrFtr.@default);
            /*CT_PPr cT_PPr = head.Paragraphs[0].GetCTP().AddNewPPr();
            CT_Spacing spacing = cT_PPr.AddNewSpacing();
            spacing.line = "0";
            spacing.lineRule = ST_LineSpacingRule.auto;*/
            CT_P ctHeaderParagraph = head.Paragraphs.First().GetCTP();
            ctHeaderParagraph.pPr = new CT_PPr();
            ctHeaderParagraph.pPr.spacing = new CT_Spacing();
            ctHeaderParagraph.pPr.spacing.line = "0";
            /*ctHeaderParagraph.pPr.autoSpaceDE = new()
            {
                val = false
            };*/
            ctHeaderParagraph.pPr.spacing.lineRule = ST_LineSpacingRule.auto;

            //name
            var headerName = head.CreateParagraph();
            headerName.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r0 = headerName.CreateRun();
            r0.FontFamily = "cambria";
            r0.FontSize = 14;
            r0.IsBold = true;
            r0.SetText(info.Name);
            
            
            //Address
            var headerAddress = head.CreateParagraph();
            headerAddress.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r1 = headerAddress.CreateRun();
            r1.FontFamily = "cambria";
            r1.FontSize = 11;
            r1.IsBold = true;
            r1.SetText(info.Address);
            //Contact Info
            var headerContact = head.CreateParagraph();
            headerContact.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r2 = headerContact.CreateRun();
            r2.FontFamily = "cambria";
            r2.FontSize = 11;
            r2.IsBold = true;
            r2.SetText($"• {info.Email} • {info.PhoneNumber} •");


            //Recipient
            var recip = doc.CreateParagraph();
            recip.Alignment = ParagraphAlignment.LEFT;
            XWPFRun recip2 = recip.CreateRun();
            recip2.FontFamily = "cambria";
            recip2.FontSize = 12;
            recip2.SetText($"Dear {recipient},");
            recip2.AddBreak(BreakClear.ALL);

            //Body
            var body = doc.CreateParagraph();
            body.Alignment = ParagraphAlignment.LEFT;
            XWPFRun body2 = body.CreateRun();
            body2.FontFamily = "cambria";
            body2.FontSize = 12;
            body2.SetText(Text);
            body2.AddBreak(BreakClear.ALL);



            return doc;
        }
    }
}
