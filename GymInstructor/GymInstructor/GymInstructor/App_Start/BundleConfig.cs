using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GymInstructor.App_Start
{
    public class BundleConfig
    {
        /// <summary>
        /// Registers bundles with the application.
        /// </summary>
        /// <param name="bundles">The bundles to register.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/admin/css").Include(
                      "~/Content/admin/css/bootstrap.min.css",
                      "~/Content/admin/css/animate.css",
                      "~/Content/admin/css/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/Content/admin/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/admin/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery-1.10.2.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Content/admin/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Content/admin/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Content/admin/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/admin/Scripts/popper.min.js",
                      "~/Content/admin/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Content/admin/Scripts/plugins/metisMenu/jquery.metisMenu.js",
                      "~/Content/admin/Scripts/plugins/pace/pace.min.js",
                      "~/Content/admin/Scripts/app/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Content/admin/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Content/admin/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Content/admin/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Content/admin/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/admin/plugins/blueimp/css/").Include(
                      "~/Content/admin/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Content/admin/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Content/admin/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/admin/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Content/admin/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Content/admin/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.js",
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Content/admin/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Content/admin/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Content/admin/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chartStyles
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Content/admin/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/admin/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/admin/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Content/admin/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/admin/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/admin/plugins/dataTables/datatables.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Content/admin/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Content/admin/Scripts/plugins/dataTables/dataTables.bootstrap4.min.js"));

            // jeditable 
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Content/admin/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/admin/plugins/jqGrid/jqGridStyles").Include(
                      "~/Content/admin/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Content/admin/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Content/admin/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/admin/plugins/codemirror/codemirror.css",
                      "~/Content/admin/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Content/admin/Scripts/plugins/codemirror/codemirror.js",
                      "~/Content/admin/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Content/admin/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Content/admin/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/admin/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Content/admin/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Content/admin/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Content/admin/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Content/admin/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Content/admin/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Content/admin/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/admin/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Content/admin/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/admin/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Content/admin/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/admin/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Content/admin/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/admin/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Content/admin/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/admin/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Content/admin/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/admin/plugins/chosen/bootstrap-chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Content/admin/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Content/admin/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/admin/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Content/admin/Scripts/plugins/steps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Content/admin/plugins/dropzone/basic.css",
                      "~/Content/admin/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Content/admin/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/admin/plugins/summernote/summernote-bs4.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Content/admin/Scripts/plugins/summernote/summernote-bs4.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Content/admin/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/admin/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker").Include(
                      "~/Content/admin/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Content/admin/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Content/admin/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/admin/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Content/admin/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/admin/plugins/jsTree/style.css"));

            // Diff
            bundles.Add(new ScriptBundle("~/plugins/diff").Include(
                      "~/Content/admin/Scripts/plugins/diff_match_patch/javascript/diff_match_patch.js",
                      "~/Content/admin/Scripts/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"));

            // Idle timer
            bundles.Add(new ScriptBundle("~/plugins/idletimer").Include(
                      "~/Content/admin/Scripts/plugins/idle-timer/idle-timer.min.js"));

            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/tinycon").Include(
                      "~/Content/admin/Scripts/plugins/tinycon/tinycon.min.js"));

            // Chartist
            bundles.Add(new StyleBundle("~/plugins/chartistStyles").Include(
                      "~/Content/admin/plugins/chartist/chartist.min.css"));

            // jsTree styles
            bundles.Add(new ScriptBundle("~/plugins/chartist").Include(
                      "~/Content/admin/Scripts/plugins/chartist/chartist.min.js"));

            // Awesome bootstrap checkbox
            bundles.Add(new StyleBundle("~/plugins/awesomeCheckboxStyles").Include(
                      "~/Content/admin/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css"));

            // Clockpicker styles
            bundles.Add(new StyleBundle("~/plugins/clockpickerStyles").Include(
                      "~/Content/admin/plugins/clockpicker/clockpicker.css"));

            // Clockpicker
            bundles.Add(new ScriptBundle("~/plugins/clockpicker").Include(
                      "~/Content/admin/Scripts/plugins/clockpicker/clockpicker.js"));

            // Date range picker Styless
            bundles.Add(new StyleBundle("~/plugins/dateRangeStyles").Include(
                      "~/Content/admin/plugins/daterangepicker/daterangepicker-bs3.css"));

            // Date range picker
            bundles.Add(new ScriptBundle("~/plugins/dateRange").Include(
                      // Date range use moment.js same as full calendar plugin 
                      "~/Content/admin/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Content/admin/Scripts/plugins/daterangepicker/daterangepicker.js"));

            // Sweet alert Styless
            bundles.Add(new StyleBundle("~/plugins/sweetAlertStyles").Include(
                      "~/Content/admin/plugins/sweetalert/sweetalert.css"));

            // Sweet alert
            bundles.Add(new ScriptBundle("~/plugins/sweetAlert").Include(
                      "~/Content/admin/Scripts/plugins/sweetalert/sweetalert.min.js"));

            // Footable Styless
            bundles.Add(new StyleBundle("~/plugins/footableStyles").Include(
                      "~/Content/admin/plugins/footable/footable.core.css", new CssRewriteUrlTransform()));

            // Footable alert
            bundles.Add(new ScriptBundle("~/plugins/footable").Include(
                      "~/Content/admin/Scripts/plugins/footable/footable.all.min.js"));

            // Select2 Styless
            bundles.Add(new StyleBundle("~/plugins/select2Styles").Include(
                      "~/Content/admin/plugins/select2/select2.min.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/plugins/select2").Include(
                      "~/Content/admin/Scripts/plugins/select2/select2.full.min.js"));

            // Masonry
            bundles.Add(new ScriptBundle("~/plugins/masonry").Include(
                      "~/Content/admin/Scripts/plugins/masonary/masonry.pkgd.min.js"));

            // Slick carousel Styless
            bundles.Add(new StyleBundle("~/plugins/slickStyles").Include(
                      "~/Content/admin/plugins/slick/slick.css", new CssRewriteUrlTransform()));

            // Slick carousel theme Styless
            bundles.Add(new StyleBundle("~/plugins/slickThemeStyles").Include(
                      "~/Content/admin/plugins/slick/slick-theme.css", new CssRewriteUrlTransform()));

            // Slick carousel
            bundles.Add(new ScriptBundle("~/plugins/slick").Include(
                      "~/Content/admin/Scripts/plugins/slick/slick.min.js"));

            // Ladda buttons Styless
            bundles.Add(new StyleBundle("~/plugins/laddaStyles").Include(
                      "~/Content/admin/plugins/ladda/ladda-themeless.min.css"));

            // Ladda buttons
            bundles.Add(new ScriptBundle("~/plugins/ladda").Include(
                      "~/Content/admin/Scripts/plugins/ladda/spin.min.js",
                      "~/Content/admin/Scripts/plugins/ladda/ladda.min.js",
                      "~/Content/admin/Scripts/plugins/ladda/ladda.jquery.min.js"));

            // Dotdotdot buttons
            bundles.Add(new ScriptBundle("~/plugins/truncate").Include(
                      "~/Content/admin/Scripts/plugins/dotdotdot/jquery.dotdotdot.min.js"));

            // Touch Spin Styless
            bundles.Add(new StyleBundle("~/plugins/touchSpinStyles").Include(
                      "~/Content/admin/plugins/touchspin/jquery.bootstrap-touchspin.min.css"));

            // Touch Spin
            bundles.Add(new ScriptBundle("~/plugins/touchSpin").Include(
                      "~/Content/admin/Scripts/plugins/touchspin/jquery.bootstrap-touchspin.min.js"));

            // Tour Styless
            bundles.Add(new StyleBundle("~/plugins/tourStyles").Include(
                      "~/Content/admin/plugins/bootstrapTour/bootstrap-tour.min.css"));

            // Tour Spin
            bundles.Add(new ScriptBundle("~/plugins/tour").Include(
                      "~/Content/admin/Scripts/plugins/bootstrapTour/bootstrap-tour.min.js"));

            // i18next Spin
            bundles.Add(new ScriptBundle("~/plugins/i18next").Include(
                      "~/Content/admin/Scripts/plugins/i18next/i18next.min.js"));

            // Clipboard Spin
            bundles.Add(new ScriptBundle("~/plugins/clipboard").Include(
                      "~/Content/admin/Scripts/plugins/clipboard/clipboard.min.js"));

            // c3 Styless
            bundles.Add(new StyleBundle("~/plugins/c3Styles").Include(
                      "~/Content/admin/plugins/c3/c3.min.css"));

            // c3 Charts
            bundles.Add(new ScriptBundle("~/plugins/c3").Include(
                      "~/Content/admin/Scripts/plugins/c3/c3.min.js"));

            // D3
            bundles.Add(new ScriptBundle("~/plugins/d3").Include(
                      "~/Content/admin/Scripts/plugins/d3/d3.min.js"));

            // Markdown Styless
            bundles.Add(new StyleBundle("~/plugins/markdownStyles").Include(
                      "~/Content/admin/plugins/bootstrap-markdown/bootstrap-markdown.min.css"));

            // Markdown 
            bundles.Add(new ScriptBundle("~/plugins/markdown").Include(
                      "~/Content/admin/Scripts/plugins/bootstrap-markdown/bootstrap-markdown.js",
                      "~/Content/admin/Scripts/plugins/bootstrap-markdown/markdown.js"));

            // Datamaps
            bundles.Add(new ScriptBundle("~/plugins/datamaps").Include(
                      "~/Content/admin/Scripts/plugins/topojson/topojson.js",
                      "~/Content/admin/Scripts/plugins/datamaps/datamaps.all.min.js"));

            // Taginputs Styless
            bundles.Add(new StyleBundle("~/plugins/tagInputsStyles").Include(
                      "~/Content/admin/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css"));

            // Taginputs
            bundles.Add(new ScriptBundle("~/plugins/tagInputs").Include(
                      "~/Content/admin/Scripts/plugins/bootstrap-tagsinput/bootstrap-tagsinput.js"));

            // Duallist Styless
            bundles.Add(new StyleBundle("~/plugins/duallistStyles").Include(
                      "~/Content/admin/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css"));

            // Duallist
            bundles.Add(new ScriptBundle("~/plugins/duallist").Include(
                      "~/Content/admin/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js"));

            // SocialButtons styles
            bundles.Add(new StyleBundle("~/plugins/socialButtonsStyles").Include(
                      "~/Content/admin/plugins/bootstrapSocial/bootstrap-social.css"));

            // Typehead
            bundles.Add(new ScriptBundle("~/plugins/typehead").Include(
                      "~/Content/admin/Scripts/plugins/typehead/bootstrap3-typeahead.min.js"));

            // Pdfjs
            bundles.Add(new ScriptBundle("~/plugins/pdfjs").Include(
                      "~/Content/admin/Scripts/plugins/pdfjs/pdf.js"));

            // Touch Punch 
            bundles.Add(new StyleBundle("~/plugins/touchPunch").Include(
                        "~/Content/admin/Scripts/plugins/touchpunch/jquery.ui.touch-punch.min.js"));

            // WOW 
            bundles.Add(new StyleBundle("~/plugins/wow").Include(
                        "~/Content/admin/Scripts/plugins/wow/wow.min.js"));

            // Text spinners styles
            bundles.Add(new StyleBundle("~/plugins/textSpinnersStyles").Include(
                      "~/Content/admin/plugins/textSpinners/spinners.css"));

            // Password meter 
            bundles.Add(new StyleBundle("~/plugins/passwordMeter").Include(
                        "~/Content/admin/Scripts/plugins/pwstrength/pwstrength-bootstrap.min.js",
                        "~/Content/admin/Scripts/plugins/pwstrength/zxcvbn.js"));

        }
    }
}