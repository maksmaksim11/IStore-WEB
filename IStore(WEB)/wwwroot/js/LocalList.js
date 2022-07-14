var localList = function (localName) {
    var local = localStorage.getItem(localName);
    var items = local ? JSON.parse(local) : new Array();

    return {
        "add": function (val) {
            var item = items.find(x => x.Id == val.Id);
            if (item == null) {
                items.push(val);
            }
            else {
                item.Count = item.Count + val.Count;
            }
            localStorage.setItem(localName, JSON.stringify(items));
        },
        "remove": function (val) {
            indx = items.indexOf(val);
            if (indx != -1) items.splice(indx, 1);
            localStorage.setItem(localName, JSON.stringify(items));
        },
        "clear": function () {
            items = null;
            localStorage.setItem(localName, null);
        },
        "items": function () {
            return items;
        },
        "replaceItems": function (val) {
            items = val;
            localStorage.setItem(localName, JSON.stringify(items));
        },
        "getHashItems": function () {
            var res = 0;
            for (x in items) {
                var hash = Math.pow(items[x].Id, items[x].Count) + items[x].Id * 5;
                res += hash;
            }
            return res;
        }
    }
}