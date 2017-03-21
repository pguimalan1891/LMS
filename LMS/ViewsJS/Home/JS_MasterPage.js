$(document).ready(function () {

    $.get($("#userMenu").text(), { "returnURL": window.location.href }, function (data) {
        if (data == "LoginExpired") {
            window.location.href = $("#lnkLogin").text();
            return;
        }
        if (data == "") {
            return;
        }
        var menus = data;
        var MainMenu = getMenus(menus, "0");
        var append = "";
        $.each(MainMenu, function (mmKey, mm) {
            var subMenu2ndLevel = getMenus(menus, mm["MenuID"]);
            if (subMenu2ndLevel.length > 0) {
                append += "<li> " +
                          "<a href='#'><i class='fa fa-th-large'></i> <span class='nav-label'>" + mm["DisplayName"] + "</span> <span class='fa arrow'></span></a>" +
                          "<ul class='nav nav-second-level'>";
                $.each(subMenu2ndLevel, function (sub2ndKey, sub2nd) {
                    var subMenu3rdLevel = getMenus(menus, sub2nd["MenuID"]);
                    if (subMenu3rdLevel.length > 0) {
                        append += "<li> " +
                                  "<a href='#'><i class='fa fa-th-large'></i> <span class='nav-label'>" + sub2nd["DisplayName"] + "</span> <span class='fa arrow'></span></a>" +
                                  "<ul class='nav nav-third-level'>";
                        $.each(subMenu3rdLevel, function (sub3rdKey, sub3rd) {
                            append += "<li><a onclick='window.location=\"" + $("#" + sub3rd["LnkAddress"]).text() + "\"'>" + sub3rd["DisplayName"] + "</a></li>";
                        });
                        append += "</ul>" +
                                  "</li>";
                    } else {
                        append += "<li><a onclick='window.location=\"" + $("#" + sub2nd["LnkAddress"]).text() + "\"'>" + sub2nd["DisplayName"] + "</a></li>";
                    }
                });

                append += "</ul>" +
                           "</li>";
            } else {
                append += "<li><a onclick='window.location=\"" + $("#" + mm["LnkAddress"]).text() + "\"'>" + mm["DisplayName"] + "</a></li>";
            }
        });
        $("#side-menu").append(append);
        initInspinia();
    });
});

function getMenus(menus, ParentID) {
    var MainMenu = [];
    var menuCount = 0;
    $.each(menus, function (menuKey, menu) {
        if (menu["ParentID"] == ParentID) {
            MainMenu[menuCount] = menu;
            menuCount += 1;
        }
    });
    return SortMenu(MainMenu);
}

function SortMenu(menu) {    
    var TempMenu = {};
    for (var x = 0; x < menu.length ; x++) {
        for (var y = 1; y < menu.length ; y++) {
            if (menu[y - 1]["Ordering"] > [menu[y]["Ordering"]]) {
                TempMenu = menu[y - 1];
                menu[y - 1] = menu[y];
                menu[y] = TempMenu;
            }
        }
    }
    return menu;
}