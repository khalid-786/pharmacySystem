
$(document).ready(function () {
    $('[placeholder]').focus(function () {

        $(this).attr('data-text', $(this).attr('placeholder'));
        $(this).attr('placeholder', '');
    }).blur(function () {

        $(this).attr('placeholder', $(this).attr('data-text'));
    });
    $("#jobedit").click(function () {

        if ($("#jobImage").val() != "") {
            //Check If The JobImage Is Valid
            var filename = document.getElementById("jobImage").value;
            var extentionImage = filename.substr(filename.lastIndexOf('.') + 1);
            var validExtention = ['jpeg', 'jpg', 'png', 'gif', 'bmp', 'JPEG', 'JPG', 'PNG', 'GIF', 'BMP']
            if ($.inArray(extentionImage, validExtention) == -1) {
                $("#jobEditFormErro").fadeIn();
                $("#jobEditFormErro").html("  غير مسموح برفع هذا النوع من الملفات  رجاءا قم بإختيار ملف صورة");
                return false;
            }
            //Check If The  JobImage Size Is Valid (2 MB) 
            var myFileSize = document.getElementById("jobImage").files[0].size / 1024 / 1024;
            if (myFileSize > 2) {
                $("#jobEditFormErro").fadeIn();
                $("#jobEditFormErro").html("حجم الملف أكبر من الحجم المسموح به رجاءا قم بإختيار ملف حجمه أقل من  2 ميقابايت");
                return false;
            }
        }
        
    });
    
});

function readUrl(input, post_image_preview) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onloadend = function (e) {
            $(post_image_preview).attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
