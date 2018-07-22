// JavaScript source code

var HospitalApp = angular.module('HospitalApp', []).run(['$rootScope', function ($rootScope) {
    $rootScope.backend = 'http://localhost/Hospital/'; 
}]).filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        if (x!=null && x!==undefined) {
            var m = x.match(re);
            if (m) return new Date(parseInt(m[1]));
            else return x;
        }
        else return x;
    };
}).directive('autoComplete', function ($timeout) {
    return function (scope, iElement, iAttrs) {
        iElement.autocomplete({
            source: scope[iAttrs.uiItems],
            select: function () {
                $timeout(function () {
                    iElement.trigger('input');
                }, 0);
            }
        });
    };
});

HospitalApp.service('filteredListService', function () {

    //this.searched = function (valLists, toSearch) {
    //    return _.filter(valLists,
    //    function (i) {
    //        /* Search Text in all 3 fields */
    //        return searchUtil(i, toSearch);
    //    });
    //};

    this.paged = function (valLists, pageSize) {
        retVal = [];
        for (var i = 0; i < valLists.length; i++) {
            if (i % pageSize === 0) {
                retVal[Math.floor(i / pageSize)] = [valLists[i]];
            } else {
                retVal[Math.floor(i / pageSize)].push(valLists[i]);
            }
        }
        return retVal;
    };

});

HospitalApp.factory('ValidationService', function ($http, $rootScope) {
    var service = {};

    service.Validate = function (model, errorModel) {
        var flagvalid = true;
        angular.forEach(model, function (entityattribute, entitykey) {
            angular.forEach(errorModel, function (item, key) {
                if (typeof item == "object") {
                    if (entitykey == key) {
                        angular.forEach(item, function (rules, rulekey) {
                            switch (rulekey) {
                                case "required":
                                    if (entityattribute == "") {
                                        item.Error = true;
                                        item.ErrorMessage = item.DisplayName + " should be filled.";
                                        flagvalid = false
                                    }
                                    else {
                                        item.Error = false;
                                    }
                                    break;
                                case "minlength":
                                    if (item.Error == false) {
                                        if (entityattribute.length < item.minlength) {
                                            item.Error = true;
                                            item.ErrorMessage = item.DisplayName + " length should be greater than " + item.minlength;
                                            flagvalid = false
                                        }
                                        else {
                                            item.Error = false;
                                        }
                                    }
                                    break;
                                case "maxlength":
                                    if (item.Error == false) {
                                        if (entityattribute.length > item.maxlength) {
                                            item.Error = true;
                                            item.ErrorMessage = item.DisplayName + " length should be less than " + item.maxlength;
                                            flagvalid = false
                                        }
                                        else {
                                            item.Error = false;
                                        }
                                    }
                                    break;
                                case "regex":
                                    if (item.Error == false) {
                                        var reg = new RegExp(item.regex);
                                        if (reg.test(entityattribute) == false) {
                                            item.Error = true;
                                            item.ErrorMessage = "Please fill valid " + item.DisplayName;
                                            flagvalid = false
                                        }
                                        else {
                                            item.Error = false;
                                        }
                                    }
                                    break;
                            }
                        });
                    }
                }
            });
        });

        return flagvalid;
    }

    service.ValidateAttribute = function (model, errorModel, Attribute) {
        var flagvalid = true;
        angular.forEach(model, function (entityattribute, entitykey) {
            if (entitykey == Attribute) {
                angular.forEach(errorModel, function (item, key) {
                    if (typeof item == "object") {
                        if (entitykey == key) {
                            angular.forEach(item, function (rules, rulekey) {
                                switch (rulekey) {
                                    case "required":
                                        if (entityattribute == "") {
                                            item.Error = true;
                                            item.ErrorMessage = item.DisplayName + " should be filled.";
                                            flagvalid = false
                                        }
                                        else {
                                            item.Error = false;
                                        }
                                        break;
                                    case "minlength":
                                        if (item.Error == false) {
                                            if (entityattribute.length < item.minlength) {
                                                item.Error = true;
                                                item.ErrorMessage = item.DisplayName + " length should be greater than " + item.minlength;
                                                flagvalid = false
                                            }
                                            else {
                                                item.Error = false;
                                            }
                                        }
                                        break;
                                    case "maxlength":
                                        if (item.Error == false) {
                                            if (entityattribute.length > item.maxlength) {
                                                item.Error = true;
                                                item.ErrorMessage = item.DisplayName + " length should be less than " + item.maxlength;
                                                flagvalid = false
                                            }
                                            else {
                                                item.Error = false;
                                            }
                                        }
                                        break;
                                    case "regex":
                                        if (item.Error == false) {
                                            var reg = new RegExp(item.regex);
                                            if (reg.test(entityattribute) == false) {
                                                item.Error = true;
                                                item.ErrorMessage = "Please fill valid " + item.DisplayName;
                                                flagvalid = false
                                            }
                                            else {
                                                item.Error = false;
                                            }
                                        }
                                        break;
                                }
                            });
                        }
                    }
                });
            }
        });

        return flagvalid;
    }

    return service;
});

function searchUtil(item, toSearch) {
    return (item.Name.toLowerCase().indexOf(toSearch.toLowerCase()) > -1 ||
        item.Email.toLowerCase().indexOf(toSearch.toLowerCase()) > -1) ? true : false;
}