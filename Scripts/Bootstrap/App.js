// JavaScript source code

var HospitalApp = angular.module('HospitalApp', []).run(['$rootScope', function ($rootScope) {
    
}]).filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        if (x!=null && x!==undefined) {
            var m = x.match(re);
            if (m) return new Date(parseInt(m[1]));
            else return null;
        }
        else return null;
    };
}); 
