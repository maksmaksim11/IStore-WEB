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

    $(".imgcoll").change(function () {

        var file = $(this)[0].files[0];
        var reader = new FileReader();

        if (this.files[0].size > 100000) { //12
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

        var element = $(this);

        reader.onload = function () {
            $(element).next().attr("src", reader.result);
        };

        if (file) {
            reader.readAsDataURL(file);
        }
        else {
            preview.src = "";
        };

    });

    $(".saveProduct").click(function () {

        var product = new Object();

        product.Id = 0;
        var d = $(".title").val();
        product.Title = $(".title").val();
        product.Type = $(".type").val();
        product.VendorCode = $(".vendorCode").val();
        product.Description = $(".description").val();

        var br = new Object();
        br.Id = 0;
        br.Name = $(".brand").val();

        product.Brand = br; //
        product.RetailPrice = $(".retailPrice").val();

        var cat = new Object();
        cat.Id = 0;
        cat.Title = $(".category").val();

        product.Category = cat;

        var pack = new Object();

        pack.Id = 0;
        pack.CountInPackage = $(".countInPackage").val();
        pack.Volume = $(".volume").val();
        pack.Weight = $(".weight").val();

        product.Package = pack;

        var groups = [];

        $(".tableBox").each(function () {

            var charact = new Object();

            charact.Id = 0;
            charact.Title = $(this).find('td').find(".charTitle").val();
            charact.Value = $(this).find('td').find(".charDescription").val();
            charact.ProductId = 0;

            groups.push(charact);
        });

        product.ProductCharacteristics = groups;
        product.CountInStorage = $(".countInStorage").val();
        product.Rating = $(".rating").val();
        product.WarrantyMonth = $(".warrantyMonth").val();
        product.Series = $(".series").val();
        product.Model = $(".model").val();

        var formData = new FormData();
        formData.append('file', $('.previewImage')[0].files[0]);

        if ($('.previewImage')[0].files[0] == null) {            
            alert("Preview photo is null");
            e.preventDefault();
        }

        $(".imgcoll").each(function () {
            formData.append('file', $(this)[0].files[0]);
        });



        formData.append('product', JSON.stringify(product));


        $.ajax({
            type: "POST",
            url: "/admin/SaveProduct",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert(data);
            }
        });
    });

    $(".newCategory").click(function () {

        $(".categoryBox").show();
    });

    $(".newBrand").click(function () {

        $(".brandBox").show();
    });
});