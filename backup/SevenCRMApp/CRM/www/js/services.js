'use strict';
angular.module('sevencrm.services', [])

    .service('userservice', function ($resource, SessionValues) {
        this.getuser = function (user) {
            return $resource(SessionValues.getConexion() + '/api/Test/GetUserByUserName?user=:username&pass=:pass',
                { username: user.user, pass: user.pass })
                .get().$promise;
        }
        this.validateuser = function (user) {
            return $resource(SessionValues.getConexion() + '/api/GnUsuar/ValidateUser?user=:user&pass=:pass',
                { user: user.user, pass: user.pass })
                .get().$promise;
        }
    })
    .service('gnConexservice', function ($resource) {
        this.getConnections = function () {
            return $resource(urlApi + '/api/GnConex/GetConnections', {})
                .get().$promise; //query : cuando la consulta retorna un arreglo y no un objeto
        }
    })
    .service('CrAgendService', function ($resource, SessionValues, $q, $http) {
        this.getAgenda = function (usu_codi, fini, fina) {
            return $resource(SessionValues.getConexion() + '/api/CrAgend/ListaActividades?usu_codi=:usu_codi&fini=:fini&fina=:fina',
                { usu_codi: usu_codi, fini: fini, fina: fina })
                .get().$promise;
        }

        this.cancelarActividad = function (emp_codi, pro_cont, act_codi, usu_eject, usu_plan, age_fini, usu_codi) {
            var deferred = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/CrAgend/CancelarActividad?emp_codi=' + emp_codi + '&pro_cont=' + pro_cont + '&act_codi=' + act_codi + '&usu_eject=' + usu_eject + '&usu_plan=' + usu_plan + '&age_fini=' + age_fini + '&usu_codi=' + usu_codi)
                .success(
                    function (response) {
                        deferred.resolve(response);
                        if (response.length > 0) {

                        }
                        if (response === 0) {
                            throw 'Error en clases de espacio!'
                        }
                    }).error(function (error, status) {
                        deferred.reject(error);
                    });
            return deferred.promise;
        }
    })
    .service('flujosservice', function ($resource, SessionValues) {
        this.getFlujos = function (emp_codi, usu_codi) {
            return $resource(SessionValues.getConexion() + '/api/Flujos/FlujosAdm?emp_codi=:emp_codi&usu_codi=:usu_codi',
                { emp_codi: emp_codi, usu_codi: usu_codi }, {
                    aprobar: { method: 'POST' },
                    rechazar: { method: 'PUT' }
                })
                .query().$promise;
        }
        this.FlujosDetalle = function (emp_codi, cas_cont, usu_codi, seg_cont) {
            return $resource(SessionValues.getConexion() + '/api/Flujos/FlujoDetalle?emp_codi=:emp&cas_cont=:cas&usu_codi=:usu&seg_cont=:seg',
                { emp: emp_codi, cas: cas_cont, usu: usu_codi, seg: seg_cont }, {
                    get: { method: 'GET' },
                    aprobar: { method: 'POST' },
                    rechazar: { method: 'PUT' }
                });
            //return $resource(SessionValues.getConexion() + '/api/Flujos/GetFlujoDetalle?emp_codi=:emp&cas_cont=:cas&usu_codi=:usu&seg_cont=:seg',
            //    { emp: emp_codi, cas: cas_cont, usu: usu_codi, seg: seg_cont }, {
            //        get: { method: 'GET' },
            //        aprobar: { method: 'POST', url: SessionValues.getConexion() + '/api/Flujos/AprobarFlujo' },
            //        rechazar: { method: 'PUT', url: SessionValues.getConexion() + '/api/Flujos/RechazarFlujo' }
            //    });
        }
        this.aprobarFlujo = function (flujo) {
            return $resource(SessionValues.getConexion() + '/api/Flujos/AprobarFlujo',
                flujo).save().$promise;
        }
    })

    .service('actividadSrv', function (SessionValues, $q, $http) {
        this.cargarEtapas = function () {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/CrAgend/CancelarActividad?emp_codi=')
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarEmpleados = function (usu_codi) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/ListarEmpleados?usu_codi=' + usu_codi)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarListaActividad = function (emp_codi) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/ListarActividades?emp_codi=' + emp_codi)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarEtapas = function (emp_codi) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/ListarEtapas?emp_codi=' + emp_codi)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarClientes = function (emp_codi, filter) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/cargarClientes?emp_codi=' + emp_codi + '&filter=' + filter)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarDClientes = function (pro_cont) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/cargarDClientes?pro_cont=' + pro_cont)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
        this.cargarContactos = function (pro_cont, dpr_codi) {
            var df = $q.defer();
            $http.get(SessionValues.getConexion() +
                '/api/Actividades/cargarContactos?pro_cont=' + pro_cont + '&dpr_codi=' + dpr_codi)
                .success(
                    function (response) {
                        df.resolve(response);
                    }).error(function (error, status) {
                        df.reject(error);
                    });
            return df.promise;
        }
    })
;