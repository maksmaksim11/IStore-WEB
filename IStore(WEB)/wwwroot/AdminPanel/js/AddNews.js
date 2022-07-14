$(function () {

    $(".previewImage").change(function (e) {

        var file = $(this)[0].files[0];
        var reader = new FileReader();

        if (this.files[0].size > 20000) { //12
            alert("File is too big!");
            this.value = "";
            e.preventDefault();
            return;
        };

        if (this.files[0].type != "image/png") {
            alert("File format not supported! Required .png");
            this.value = "";
            e.preventDefault();
            return;
        };

        reader.onload = function () {
            $(".previewImageView").attr("src", reader.result);
        };

        if (file) {
            reader.readAsDataURL(file);
        }
        else {
            preview.src = "";
        };

    });

    
    $(".saveNews").click(function () {

        var news = new Object();
        
        news.Id = 0;
        news.Data = $(".data").val();
        news.Text = $("textarea").val();
       
        var formData = new FormData();
        formData.append('file', $('.previewImage')[0].files[0]);

        if ($('.previewImage')[0].files[0] == null) {
            alert("Preview photo is null");
            e.preventDefault();
        }

        formData.append('news', JSON.stringify(news));


        $.ajax({
            type: "POST",
            url: "/admin/AddNews",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert(data);
            }
        });
    });

    
});