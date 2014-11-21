$(function () {
    $('#file_upload').uploadify({
        
        'swf': "/Scripts/uploadify.swf",
       
        'uploader': '/Home/FileUpload/'+@Url.Action("FileUpload", "Home", new { groupID = jsmodel.Item1.GroupID})',
        'onUploadSuccess': function (file, data, response) {
            //data is whatever you return from the server
            //we're sending the URL from the server so we append this as an image to the #uploaded div
            $("#uploaded").append("<img src='" + data + "' alt='Uploaded Image' />");
        }
    });
});
