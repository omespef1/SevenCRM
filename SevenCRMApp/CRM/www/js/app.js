// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('sevencrm', ['ionic', 'ngCordova', 'ngResource', 'ionic-datepicker', 'sevencrm.controllers',
    'ionic-material', 'ionMdInput', 'sevencrm.routes', 'sevencrm.services', 'sevencrm.helpers'])

    .constant('webapi', { serviceApi: urlApi })

    .config(function ($httpProvider, $provide, ionicDatePickerProvider) {
        var datePickerObj = {
            inputDate: new Date(),
            titleLabel: 'Seleccione una Fecha',
            setLabel: 'OK',
            todayLabel: 'Hoy',
            closeLabel: 'Cerrar',
            mondayFirst: false,
            weeksList: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
            monthsList: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sept", "Oct", "Nov", "Dic"],
            templateType: 'popup',
            from: new Date(2000, 1, 1),
            //to: new Date(2020, 1, 1),
            showTodayButton: true,
            dateFormat: 'dd-MM-yyyy',
            closeOnSelect: false,
            disableWeekdays: []
        };
        ionicDatePickerProvider.configDatePicker(datePickerObj);

        $provide.factory('myHttpInterceptor', function ($q, $rootScope) {
            return {
                // optional method
                'request': function (config) {
                    // do something on success
                    $rootScope.$broadcast('loading:show');
                    return config;
                },
                // optional method
                'requestError': function (rejection) {
                    // do something on error
                    $rootScope.$broadcast('loading:hide');
                    //if (canRecover(rejection)) {
                    //    return responseOrNewPromise
                    //}
                    return $q.reject(rejection);
                },
                // optional method
                'response': function (response) {
                    // do something on success
                    $rootScope.$broadcast('loading:hide');
                    return response;
                },
                // optional method
                'responseError': function (rejection) {
                    // do something on error
                    $rootScope.$broadcast('loading:hide');
                    //$ionicPopup.alert({
                    //    title: 'Seven CRM',
                    //    template: 'Ocurrió un error: ' + rejection
                    //});
                    //if (canRecover(rejection)) {
                    //    return responseOrNewPromise
                    //}
                    return $q.reject(rejection);
                }
            };
        });

        $httpProvider.interceptors.push('myHttpInterceptor');
    })

    .run(function ($ionicPlatform, $rootScope, $ionicLoading, $ionicPopup) {
        $ionicPlatform.ready(function () {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)
            if (window.cordova && window.cordova.plugins.Keyboard) {
                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            }
            if (window.StatusBar) {
                // org.apache.cordova.statusbar required
                StatusBar.styleDefault();
            }
            //$cordovaPlugin.someFunction().then(function (success) {
            //    console.log(success);
            //}, function (err) {
            //    console.log(err);
            //});
            //$rootScope.$ionicGoBack = function () {
            //    $ionicHistory.goBack();
            //    //$ionicHistory.clearCache();
            //};
        });
        $ionicPlatform.registerBackButtonAction(function (event) {
            $ionicPopup.confirm({
                title: 'Salir',
                template: '¿Desea salir de la aplicación?'
            }).then(function (res) {
                if (res) {
                    navigator.app.exitApp();
                }
            });
        }, 100);
        $rootScope.$on('loading:show', function () {
            $ionicLoading.show({
                template: '<div class="loader"><svg class="circular"><circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10"/></svg></div>'
            });
        });
        $rootScope.$on('loading:hide', function () {
            $ionicLoading.hide();
        });
    })

;