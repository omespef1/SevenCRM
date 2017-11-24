'use strict';
angular.module('sevencrm.helpers', [])
    /*FACTORIES*/
    .factory('SessionValues', function () {
        var _user = null;
        var session = {};
        session.setUser = function (user) {
            _user = user;
            if (session.isRemember())
                localStorage.setItem('CurrentUser', JSON.stringify(user));
        }
        session.getUser = function () {
            if (session.isRemember())
                _user = JSON.parse(localStorage.getItem('CurrentUser'));
            return _user;
        }
        session.getConexion = function () {
            return localStorage.getItem('Cnx_Ipsr');
        }
        session.setConexion = function (Cnx_Ipsr) {
            localStorage.setItem('Cnx_Ipsr', Cnx_Ipsr);;
        }
        session.setRemember = function (value) {
            localStorage.setItem('remember', value + '');
        }
        session.isRemember = function () {
            return localStorage.getItem('remember') != null
                && localStorage.getItem('remember') == "true";
        }
        session.flujoSeleccionado = null;
        return session;
    })
    .factory('Utils', function () {
        var utils = {};
        utils.addUserHistorial = function (user) {
            var _userList = [];
            if (localStorage.getItem('UserHistorial') != null) {
                _userList = JSON.parse(localStorage.getItem('UserHistorial'));
            }
            if (user != null && !_userList.usercontains(user.Usu_Codi)) {
                if (_userList.length > 2)
                    _userList.splice(0, 1);
                _userList.push(user);
            }
            localStorage.setItem('UserHistorial', JSON.stringify(_userList));
        }
        utils.setUserHistorial = function (historial) {
            localStorage.setItem('UserHistorial', JSON.stringify(historial));
        }
        utils.getUserHistorialItems = function () {
            //localStorage.removeItem('UserHistorial');
            var _userList = [];
            if (localStorage.getItem('UserHistorial') != null) {
                _userList = JSON.parse(localStorage.getItem('UserHistorial'));
            }
            return _userList;
        }
        utils.getUserFromHistorial = function (usu_codi) {
            var us = null;
            var _userList = [];
            if (localStorage.getItem('UserHistorial') != null) {
                _userList = JSON.parse(localStorage.getItem('UserHistorial'));
            }
            return _userList.getuser(usu_codi);
        }
        return utils;
    })

    .factory('SevenMessage', function ($ionicPopup) {
        var svMsg = {};
        svMsg.Show = function (message) {
            $ionicPopup.alert({
                title: 'Seven CRM',
                template: message
            });
        }
        return svMsg;
    })
;