Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}
Array.prototype.usercontains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i].Usu_Codi === obj) {
            return true;
        }
    }
    return false;
}
Array.prototype.getuser = function (usu_codi) {
    var i = this.length;
    while (i--) {
        if (this[i].Usu_Codi === usu_codi) {
            return this[i];
        }
    }
    return null;
}