$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Category/GetCategoryJson",
        dataType: 'json',
        success: function (data) {
            $.each(data, function (i) {
                if (data[i].subcategories == 0) {
                    $("#category").append("<li class='menu-item'> <a href='/Products/" + data[i].title + "' class='tanajil-menu-item-title' title=" + data[i].title + ">" + data[i].title + "</a></li>");
                    $(".list-categories").append("<li class='menu-item'> <input type = 'checkbox' id = 'cb" + i + "'><label for='cb" + i + "' class='label-text'>" + data[i].title + "</label></li >");
                }
                else {
                    $("#category").append("<li class='menu-item menu-item-has-children'><a href='/Products/" + data[i].title + "' class='tanajil-menu-item-title' title=" + data[i].subcategories[i].title + ">" + data[i].subcategories[j].title + "</a><span class='toggle-submenu'></span><ul  role='menu' class='category-submenu-" + data[i].title+" submenu'></ul></li>");
                    $.each(data[i].subcategories, function (j) {
                        $(".category-submenu-" + data[i].title).append("<li class='menu-item'><a href='/Products/" + data[i].title + "/" + data[i].subcategories[j].title + "' class='tanajil-item-title' title=" + data[i].subcategories[j].title + ">" + data[i].subcategories[j].title + "</a></li>");
                    })
                }
            });
        },
    });
});

