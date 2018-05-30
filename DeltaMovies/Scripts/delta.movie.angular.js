/// <reference path="angular.min.js" />
$.ShowLoader = function () {
    $('.page-loader-wrapper').fadeIn();
};
$.HideLoader = function () {
    setTimeout(function () { $('.page-loader-wrapper').fadeOut(); }, 25);
};
kendo.culture("en-IN");
var globalSettings = {
    TimeOut: 10000
};
var app = angular.module("DeltaMovie", ['ngRoute', 'ngSanitize', 'kendo.directives']);

app.config(function ($routeProvider, $locationProvider, $httpProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Movies',
            controller: 'MoviesController'
        })
        .when('/Movies', {
             templateUrl: '/Movies',
             controller: 'MoviesController'
         })
        .when('/Movie/Create', {
            templateUrl: '/Movie/Create',
            controller: 'MovieCurdController'
        })
        .when('/Movie/Edit/:name/:id', {
            templateUrl: function (parameter) {
                return '/Movie/Edit/' + parameter.name + '/' + parameter.id;
            },
            controller: 'MovieCurdController'
        })

        .when('/Actor/Edit/:name/:id', {
            templateUrl: function (parameter) {
                return '/Actor/Edit/' + parameter.name + '/' + parameter.id;
            },
            controller: 'ActorCurdController'
        })
        .when('/Actors', {
            templateUrl: '/Actors',
            controller: 'ActorListController'
        })

        .when('/Producer/Edit/:name/:id', {
            templateUrl: function (parameter) {
                return '/Producer/Edit/' + parameter.name + '/' + parameter.id;
            },
            controller: 'ProducerCurdController'
        })
        .when('/Producers', {
            templateUrl: '/Producers',
            controller: 'ProducerListController'
        })
        .otherwise({
            templateUrl: '/Error/PageNotFound',
            controller: 'ErrorController'
        });
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    $locationProvider.html5Mode(false).hashPrefix('');
});
app.run(['$window', '$rootScope', '$location', '$templateCache', function ($window, $rootScope, $location, $templateCache) {
    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        if (next.$$route.controller != 'MoviesController') {
            $.ShowLoader();
        }
    });
    $rootScope.$on("$routeChangeSuccess", function (event, next, current) {
    });
    $rootScope.$on('$viewContentLoaded', function () {
        $.HideLoader();
        $templateCache.removeAll();
    });
}]);
app.directive('errorSrc', function () {
    return {
        link: function (scope, element, attrs) {
            element.bind('error', function () {
                if (attrs.src != attrs.errorSrc) {
                    attrs.$set('src', attrs.errorSrc);
                }
            });
        }
    }
});

app.controller('MoviesController', function ($rootScope, $scope, $http, $location) {
    $rootScope.header = "Movies | ";
    $scope.redirectToMovie = function (data) {
        var name = data.Name.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '').replace(/[^A-Z0-9]+/ig, "-");
        var url = '#/Movie/Edit/' + name + "/" + data.MovieId;
        return url;
    };
    $scope.previewProducer = function (name, id) {
        var name = name.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '').replace(/[^A-Z0-9]+/ig, "-");
        var url = '/Producer/Preview/' + name + "/" + id;
        return url;
    };
    $scope.previewActor = function (name, id) {
        var name = name.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '').replace(/[^A-Z0-9]+/ig, "-");
        var url = '/Actor/Preview/' + name + "/" + id;
        return url;
    };
    $scope.AllMovies = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/Movies/All",
                dataType: "json"
            }
        },
        schema: {
            data: function (result) {
                return result.Items;
            },
            total: function (result) {
                return result.Items == null ? 0 : result.Items.length
            }
        },
        pageSize: 10
    });
    $scope.MoviesPagerOptions = {
        refresh: true,
        numeric: true,
        refresh: false,
        pageSize: 10,
        buttonCount: 5,
        pageSizes: [10, 25, 50],
        previousNext: true,
        input: false,
        info: true
    }
    $scope.moviesOnDataBound = function (e) {
        if (e.items.length == 0) {
            $("#moviesInformation").html("<p class='k-no-data'>No Movie(s) found!</p>");
        }
    }

    $scope.ProducerInfoWindow = function (dataItem) {
        var kendoWindow = $("<div id='" + dataItem.ProducerId + "'></div>");
        $("#producerInfoWindow").append(kendoWindow);
        kendoWindow.html(kendo.format($("#associatesTemplates").html(), dataItem.Name, (dataItem.Sex == 'M' ? 'Male' : "Female"), kendo.toString(new Date(dataItem.DOB), "D"), dataItem.Bio)).kendoWindow({
            width: "600px",
            title: kendo.format("Producer Info : {0}", dataItem.Name),
            visible: false,
            modal: true,
            resizable: false,
            actions: [
                "Close"
            ],
            close: function () {
                $("#producerInfoWindow").empty();
            }
        }).data("kendoWindow").center().open();
    }
    $scope.ActorInfoWindow = function (dataItem) {
        var kendoWindow = $("<div id='" + dataItem.ActorId + "'></div>");
        $("#actorInfoWindow").append(kendoWindow);
        kendoWindow.html(kendo.format($("#associatesTemplates").html(), dataItem.Name, (dataItem.Sex == 'M' ? 'Male' : "Female"), kendo.toString(new Date(dataItem.DOB), "D"), dataItem.Bio)).kendoWindow({
            width: "600px",
            title: kendo.format("{1} Info : {0}", dataItem.Name, (dataItem.Sex == 'M' ? 'Actor' : "Actress")),
            visible: false,
            modal: true,
            resizable: false,
            actions: [
                "Close"
            ],
            close: function () {
                $("#actorInfoWindow").empty();
            }
        }).data("kendoWindow").center().open();
    }
});

app.controller('MovieCurdController', function ($rootScope, $scope, $http, $location, $sce, $routeParams, $timeout) {
    $rootScope.header = "Movie | ";
    $rootScope.Name = "Movie";
    $scope.MovieName = '';
    $scope.YearOfRelease = new Date();
    $scope.ProducedBy = 0;
    $scope.PosterImage = '';
    $scope.responseMessage = '';
    $scope.Plot = '';
    $scope.validationMessage = "";
    $scope.validationClass = "";
    $scope.ButtonName = 'Save';

    $scope.MovieId = typeof ($routeParams.id) == 'undefined' ? 0 : $routeParams.id;

    if ($scope.MovieId > 0) {
        $.ShowLoader();
        var movieId = $scope.MovieId;
        $http.get('/api/Movies/GetMovieBy/' + movieId).then(function (result) {
            $.HideLoader();
            var response = result.data;
            if (response.Status == 2) {
                $scope.ButtonName = 'Update';
                //data found to display.
                $scope.MovieName = response.Item.Name;
                $scope.PrevMovieName = response.Item.Name;
                $scope.YearOfRelease = new Date(response.Item.YearOfRelease, 01, 01);
                if (response.Item.ActorsInCast.length > 0) {
                    var actors = [];
                    $.each(response.Item.ActorsInCast, function () {
                        actors.push(this.ActorId);
                    });
                    $scope.Actors = actors;
                } else {
                    $scope.Actors = [];
                }
                $scope.ProducedBy = response.Item.ProducedBy;
                $scope.PosterImage = response.Item.Poster;
                if (response.Item.PosterImageInfo != null && response.Item.PosterImageInfo.FileSize > 0) {
                    var posterImageInfo = response.Item.PosterImageInfo;
                    $scope.MoviePoster = [{
                        name: posterImageInfo.FileName,
                        extension: posterImageInfo.FileExtension,
                        size: posterImageInfo.FileSize
                    }];
                } else {
                    $scope.MoviePoster = [];
                }
                $scope.Plot = response.Item.Plot;
            } else {
                $scope.MoviePoster = [];
                $scope.Actors = [];
                $scope.MovieId = 0;
                $scope.responseMessage = $sce.trustAsHtml(response.Message);
            }
        });
    } else {
        $scope.MoviePoster = [];
        $scope.Actors = [];
    }


    $scope.yearOfReleaseOptions = {
        start: "decade",
        depth: "decade"
    };

    $scope.producerDataSource = {
        type: "json",
        transport: {
            read: {
                dataType: "json",
                url: "/api/Producer/BindProducers",
            }
        },
        schema: {
            data: function (result) {
                return $.isEmptyObject(result) ? '' : result;
            }
        }
    };

    $scope.producerOptions = {
        dataSource: $scope.producerDataSource,
        dataTextField: "Text",
        dataValueField: "Value",
        valueTemplate: '<span class="k-state-default select-template"><p>{{dataItem.Text}} ({{dataItem.Sex}})</p><p><b>Date Of Birth : </b>{{dataItem.DOB}}</p></span>',
        template: '<span class="k-state-default select-template"><p>{{dataItem.Text}} ({{dataItem.Sex}})</p><p><b>Date Of Birth : </b>{{dataItem.DOB}}</p></span>'
    }

    $scope.actorDataSource = {
        type: "json",
        transport: {
            read: {
                dataType: "json",
                url: "/api/Actor/BindActors",
            }
        },
        schema: {
            data: function (result) {
                return $.isEmptyObject(result) ? '' : result;
            }
        }
    };

    $scope.actorOptions = {
        placeholder: "-- Select Actor(s) --",
        dataTextField: "Text",
        dataValueField: "Value",
        valuePrimitive: true,
        autoBind: false,
        autoClose: false,
        dataSource: $scope.actorDataSource,
        tagTemplate: '<span class="k-state-default select-template"><p>{{dataItem.Text}} ({{dataItem.Sex}})</p><p><b>Date Of Birth : </b>{{dataItem.DOB}}</p></span>',
        itemTemplate: '<span class="k-state-default select-template"><p>{{dataItem.Text}} ({{dataItem.Sex}})</p><p><b>Date Of Birth : </b>{{dataItem.DOB}}</p></span>'
    }
    $scope.onPosterImageSuccess = function (e) {
        $scope.PosterImage = null;
        if (e.operation == 'upload') {
            if (e.response != null && e.response.FileSize > 0) {
                var posterImageInfo = e.response;
                $scope.PosterImage = posterImageInfo.FileName;
                $("#PosterImage").data('kendoUpload').getFiles()[0].name = posterImageInfo.FileName;
            }
        }
    };
    $scope.onPosterImageRemove = function (e) {
        $scope.PosterImage = null;
    };

    $scope.SaveMovieInfo = function (e) {
        e.preventDefault();
        var validator = $scope.validator.validate();
        if (validator) {
            if (kendo.parseDate($scope.YearOfRelease) == null) {
                $scope.validationMessage = "Year Of Release is required!";
                return false;
            } else {
                $scope.validationMessage = '';
            }

            $.ShowLoader();
            var formData = {
                MovieId: $scope.MovieId,
                MovieName: $scope.MovieName,
                YearOfRelease: kendo.toString(kendo.parseDate($scope.YearOfRelease), 'yyyy'),
                Plot: $scope.Plot,
                PosterImage: $scope.PosterImage,
                Actors: $scope.Actors,
                ProducedBy: $scope.ProducedBy
            }
            $http({
                method: 'POST',
                url: '/api/Movies/CreateEdit',
                data: formData
            }).then(function successCallback(response) {
                $.HideLoader();
                $scope.responseMessage = $sce.trustAsHtml(response.data.Message);
                $timeout(function () {
                    $scope.responseMessage = $sce.trustAsHtml('');
                }, globalSettings.TimeOut);
                if (response.data.Status == 2) {
                    $scope.PrevMovieName = $scope.MovieName;
                    if ($scope.MovieId == 0) {
                        $scope.MovieId = 0;
                        $scope.MovieName = '';
                        $scope.YearOfRelease = null;
                        $scope.Actors = [];
                        $scope.ProducedBy = 0;
                        $scope.PosterImage = '';
                        $scope.Plot = '';
                        $scope.MoviePoster = null;

                        $(".k-upload-files").remove();
                        $(".k-upload-status").remove();
                        $(".k-upload.k-header").addClass("k-upload-empty");
                        $(".k-upload-button").removeClass("k-state-focused");
                    }
                }
            });
        }
    }
});

app.controller('ActorCurdController', function ($rootScope, $scope, $http, $location, $sce, $timeout) {
    $rootScope.header = "Actor | ";
    $rootScope.Name = "Actor";

    $scope.ActorName = '';
    $scope.ASex = 'M';
    $scope.ADOB = '';
    $scope.Bio = '';
    $scope.actorResponseMessage = '';
    $scope.validationMessage = '';

    $scope.SaveActorInfo = function (e) {
        e.preventDefault();
        var validator = $scope.validator.validate();
        if (validator) {
            if (kendo.parseDate($scope.ADOB) == null) {
                $scope.validationMessage = 'Date Of Birth is required!';
                return false;
            } else {
                $scope.validationMessage = '';
            }

            $.ShowLoader();
            var formData = {
                ActorName: $scope.ActorName,
                Sex: $scope.ASex,
                DOB: kendo.toString(kendo.parseDate($scope.ADOB), 'MM/dd/yyyy'),
                Bio: $scope.Bio,
                NameConfirm: $scope.NameConfirm
            }
            $http({
                method: 'POST',
                url: '/api/Actor/Create',
                data: formData
            }).then(function successCallback(response) {
                $.HideLoader();
                if (response.data.Status == 1) {
                    if (confirm(response.data.Message)) {
                        $scope.NameConfirm = true;
                        $scope.SaveActorInfo(e);
                    } else { $scope.NameConfirm = false; }
                } else {
                    $scope.NameConfirm = false;
                    $scope.actorResponseMessage = $sce.trustAsHtml(response.data.Message);
                    $timeout(function () {
                        $scope.actorResponseMessage = $sce.trustAsHtml('');
                    }, globalSettings.TimeOut);
                }

                if (response.data.Status == 2) {
                    $("#Actors").data('kendoMultiSelect').dataSource.read();
                    $scope.ActorName = '';
                    $scope.ASex = 'M';
                    $scope.ADOB = '';
                    $scope.Bio = '';
                    $scope.NameConfirm = false;
                }
            });
        }
    }
});

app.controller('ProducerCurdController', function ($rootScope, $scope, $http, $location, $sce, $timeout) {
    $rootScope.header = "Producer | ";
    $rootScope.Name = "Producer";

    $scope.ProducerName = '';
    $scope.PSex = 'M';
    $scope.PDOB = '';
    $scope.Bio = '';
    $scope.producerResponseMessage = '';
    $scope.validationMessage = '';

    $scope.SaveProducerInfo = function (e) {
        e.preventDefault();
        var validator = $scope.validator.validate();
        if (validator) {
            if (kendo.parseDate($scope.PDOB) == null) {
                $scope.validationMessage = 'Date Of Birth is required!';
                return false;
            } else {
                $scope.validationMessage = '';
            }
            $.ShowLoader();
            var formData = {
                ProducerName: $scope.ProducerName,
                Sex: $scope.PSex,
                DOB: kendo.toString(kendo.parseDate($scope.PDOB), 'MM/dd/yyyy'),
                Bio: $scope.Bio,
                NameConfirm: $scope.NameConfirm
            }
            $http({
                method: 'POST',
                url: '/api/Producer/Create',
                data: formData
            }).then(function successCallback(response) {
                $.HideLoader();
                if (response.data.Status == 1) {
                    if (confirm(response.data.Message)) {
                        $scope.NameConfirm = true;
                        $scope.SaveProducerInfo(e);
                    } else { $scope.NameConfirm = false; }
                }
                else {
                    $scope.NameConfirm = false;
                    $scope.producerResponseMessage = $sce.trustAsHtml(response.data.Message);
                    $timeout(function () {
                        $scope.producerResponseMessage = $sce.trustAsHtml('');
                    }, globalSettings.TimeOut);
                }

                if (response.data.Status == 2) {
                    $("#ProducedBy").data('kendoDropDownList').dataSource.read();
                    $scope.ProducerName = '';
                    $scope.PSex = 'M';
                    $scope.PDOB = '';
                    $scope.Bio = '';
                    $scope.NameConfirm = false;
                }
            });
        }
    }
});

app.controller('ErrorController', function ($rootScope, $scope, $http, $location) {
    $rootScope.header = "Error | ";
    $rootScope.Name = "Error";
});