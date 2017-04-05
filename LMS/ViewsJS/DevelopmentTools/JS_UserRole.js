$(document).ready(function () {
    loadMenus("");
});

function loadMenus(roleID) {
    $("#tblUserMenus tbody").children().remove();
    $.get("DTSecurityManager/FetchUserMenus", { "RoleID": roleID }, function (data) {
        var menus = data;
        var MainMenu = getMenus(menus, "0");
        var append = "";
        $.each(MainMenu, function (mmKey, mm) {
            var subMenu2ndLevel = getMenus(menus, mm["MenuID"]);
            if (subMenu2ndLevel.length > 0) {
                append += "<tr><td><div class='row'><div class='col-md-10'><label>" + mm["DisplayName"] + "</label><input class='pull-right' type='checkbox'/></div></div></td></tr>"
                $.each(subMenu2ndLevel, function (sub2ndKey, sub2nd) {
                    var subMenu3rdLevel = getMenus(menus, sub2nd["MenuID"]);
                    if (subMenu3rdLevel.length > 0) {
                        append += "<tr><td><div class='row'><div class='col-md-offset-1 col-md-9'><label>" + sub2nd["DisplayName"] + "</label><input class='pull-right' type='checkbox'/></div></div></td></tr>"
                        $.each(subMenu3rdLevel, function (sub3rdKey, sub3rd) {
                            append += "<tr><td><div class='row'><div class='col-md-offset-2 col-md-8'><label>" + sub3rd["DisplayName"] + "</label><input class='pull-right' type='checkbox'/></div></div></td></tr>"
                        });                        
                    } else {
                        append += "<tr><td><div class='row'><div class='col-md-offset-1 col-md-9'><label>" + sub2nd["DisplayName"] + "</label><input class='pull-right' type='checkbox'/></div></div></td></tr>"
                    }
                });

                append += "</ul>" +
                           "</li>";
            } else {
                append += "<tr><td><div class='row'><div class='col-md-10'><label>" + mm["DisplayName"] + "</label><input class='pull-right' type='checkbox'/></div></div></td></tr>"
                
            }
        });
        $("#tblUserMenus tbody").append(append);
    });
}

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