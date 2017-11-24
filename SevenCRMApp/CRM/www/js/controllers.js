/* global angular, document, window */
'use strict';

angular.module('sevencrm.controllers', [])

    .controller('AppCtrl', function ($scope, $state, SessionValues, $ionicHistory) {
        if (SessionValues.getUser() == null) {
            $state.go("login", {}, {
                reload: true
            });
        }
        // Form data for the login modal
        $scope.loginData = {};
        $scope.user = SessionValues.getUser();
        $scope.isExpanded = false;
        $scope.hasHeaderFabLeft = false;
        $scope.hasHeaderFabRight = false;
        $scope.btnSave = false;

        var navIcons = document.getElementsByClassName('ion-navicon');
        if (navIcons != null && navIcons != undefined) {
            for (var i = 0; i < navIcons.length; i++) {
                navIcons.addEventListener('click', function () {
                    this.classList.toggle('active');
                });
            }
        }


        ////////////////////////////////////////
        // Layout Methods
        ////////////////////////////////////////

        $scope.hideNavBar = function () {
            document.getElementsByTagName('ion-nav-bar')[0].style.display = 'none';
        };

        $scope.showNavBar = function () {
            document.getElementsByTagName('ion-nav-bar')[0].style.display = 'block';
        };

        $scope.noHeader = function () {
            var content = document.getElementsByTagName('ion-content');
            for (var i = 0; i < content.length; i++) {
                if (content[i].classList.contains('has-header')) {
                    content[i].classList.toggle('has-header');
                }
            }
        };

        $scope.setExpanded = function (bool) {
            $scope.isExpanded = bool;
        };

        $scope.setHeaderFab = function (location) {
            var hasHeaderFabLeft = false;
            var hasHeaderFabRight = false;

            switch (location) {
                case 'left':
                    hasHeaderFabLeft = true;
                    break;
                case 'right':
                    hasHeaderFabRight = true;
                    break;
            }

            $scope.hasHeaderFabLeft = hasHeaderFabLeft;
            $scope.hasHeaderFabRight = hasHeaderFabRight;
        };

        $scope.hasHeader = function () {
            var content = document.getElementsByTagName('ion-content');
            for (var i = 0; i < content.length; i++) {
                if (!content[i].classList.contains('has-header')) {
                    content[i].classList.toggle('has-header');
                }
            }

        };

        $scope.hideHeader = function () {
            $scope.hideNavBar();
            $scope.noHeader();
        };

        $scope.showHeader = function () {
            $scope.showNavBar();
            $scope.hasHeader();
        };

        $scope.logout = function () {
            SessionValues.setRemember(false);
            SessionValues.setUser(null);
            $state.go("login", {}, {
                reload: true
            });
        }

        $scope.clearFabs = function () {
            var fabs = document.getElementsByClassName('button-fab');
            if (fabs.length && fabs.length > 1) {
                fabs[0].remove();
            }
        };

        $scope.backHome = function () {
            $ionicHistory.clearHistory();
            $ionicHistory.clearCache();

            $ionicHistory.nextViewOptions({
                disableBack: true
            });
            $state.go("crm.profile", {
                reload: true
            });
        }
    })

    .controller('LoginCtrl', function ($scope, $state, $timeout, $stateParams, $rootScope,
        $ionicPopup, ionicMaterialInk, userservice, SessionValues, Utils, $ionicModal,
        gnConexservice, SevenMessage) {
        $scope.version = version;
        $scope.Conexiones = [];
        $scope.ConexionSeleccionada = null;
        $scope.message_conexion = "";
        $scope.loginData = {
            user: '',
            pass: '',
            remember: false
        };
        $ionicModal.fromTemplateUrl('templates/conexiones.html', {
            scope: $scope,
            animation: 'slide-in-up',
            controller: 'empresasCtrl',
            resolve: {
                Items: function () {
                    return null;
                }
            }
        }).then(function (modal) {
            $scope.modalConexion = modal;
            $scope.init();
        });
        $scope.showModalConexion = function () {
            $scope.modalConexion.show();
        };
        $scope.closeModalConexion = function () {
            $scope.modalConexion.hide();
        };
        // Cleanup the modal when we're done with it!
        $scope.$on('$destroy', function () {
            $scope.modalConexion.remove();
        });
        // Execute action on hide modal
        $scope.$on('modal.hidden', function () {
            // Execute action
        });
        // Execute action on remove modal
        $scope.$on('modal.removed', function () {
            // Execute action
        });
        $scope.isHistorial = false;
        $scope.init = function () {
          console.log(SessionValues.getConexion());
            if (SessionValues.getConexion() == null) {
                $scope.showModalConexion();
            }
            if (SessionValues.isRemember()) {
                /*cargamos en la variable de sesion el usuario*/
                $state.go("crm.profile", {}, {
                    reload: true
                });
            }
            var userHistorial = Utils.getUserHistorialItems();
            if (userHistorial != null && userHistorial.length > 0 && isUserHistorial) {
                $scope.isHistorial = true;
                $scope.userHistorial = userHistorial;
            }
            $timeout(function () {
                ionicMaterialInk.displayEffect();
            }, 300);
        }
        $scope.initConexion = function () {
            if (SessionValues.getConexion() == null) {
                gnConexservice.getConnections().then(function (response) {
                    if (response.State) {
                        $scope.Conexiones = response.ObjResult;
                    } else {
                        SevenMessage.Show('Error al consultar las Conexiones: ' + response.Message);
                    }
                }, function (err) {
                    SevenMessage.Show('Error al consultar las Conexiones: ' + err);
                });
            }
        }
        $scope.aceptarConexion = function (Cnx_Ipsr) {
            if (Cnx_Ipsr == null) {
                $scope.message_conexion = 'Debe seleccionar una conexión para empezar.';
                return;
            }
            SessionValues.setConexion(Cnx_Ipsr);
            $scope.closeModalConexion();
        }
        $scope.doLogin = function () {
            userservice.validateuser($scope.loginData).then(function (response) {
                if (response.State) {
                    SessionValues.setRemember($scope.loginData.remember);
                    SessionValues.setUser(response.ObjResult);
                    Utils.addUserHistorial(response.ObjResult);
                    if (response.ObjResult.Solo_Flujos == "S") {
                        $state.go("crm.flujos", {}, {
                            reload: true
                        });
                    } else {
                        $state.go("crm.profile", {}, {
                            reload: true
                        });
                    }
                } else {
                    SevenMessage.Show('No se pudo iniciar sesión: ' + response.Message);
                }
            }, function (err) {
                SevenMessage.Show('Ocurrió un error al iniciar sesión: ' + err.data.MessageDetail);
            });
        };
        $scope.doNewSessionLogin = function () {
            $scope.isHistorial = false;
        }
        $scope.sessionItemClick = function (user) {
            $state.go("userlogin", {}, {
                reload: true
            });
        }
        $scope.onHold = function (index) {
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Desea eliminar el usuario seleccionado?'
            }).then(function (res) {
                if (res) {
                    var uH = $scope.userHistorial;
                    uH.splice(index, 1);
                    Utils.setUserHistorial(uH);
                    if (uH.length == 0)
                        $scope.isHistorial = false;
                }
            }, function (err) {
            });
        }
    })

    .controller('UserLoginCtrl', function ($scope, $state, $stateParams, userservice, SevenMessage,
        SessionValues, Utils, $ionicPopup, $rootScope, $timeout, ionicMaterialInk, $ionicHistory) {
        $scope.version = version;
        $scope.loginData = {
            user: $stateParams.usu_codi,
            pass: '',
            remember: false
        };
        $scope.user = Utils.getUserFromHistorial($stateParams.usu_codi);
        $scope.init = function () {
            if (!isUserHistorial) {
                $state.go("login", {}, {
                    reload: true
                });
            }
            $timeout(function () {
                ionicMaterialInk.displayEffect();
            }, 300);
        }
        $scope.isHistorial = false;
        $scope.doLogin = function () {
            userservice.validateuser($scope.loginData).then(function (response) {
                if (response.State) {
                    SessionValues.setRemember($scope.loginData.remember);
                    SessionValues.setUser(response.ObjResult);
                    Utils.addUserHistorial(response.ObjResult);
                    if (response.ObjResult.Solo_Flujos == "S") {
                        $state.go("crm.flujos", {}, {
                            reload: true
                        });
                    } else {
                        $state.go("crm.profile", {}, {
                            reload: true
                        });
                    }
                } else {
                    SevenMessage.Show('No se pudo iniciar sesión: ' + response.Message);
                }
            }, function (err) {
                SevenMessage.Show('Ocurrió un error al iniciar sesión: ' + err.data.MessageDetail);
            });
        };
        $scope.toLogin = function () {
            $ionicHistory.clearHistory();
            $ionicHistory.clearCache();
            $state.go("login", {}, {
                reload: true
            });
        }
    })

    .controller('ProfileCtrl', function ($scope, $stateParams, $timeout, ionicMaterialMotion,
        ionicMaterialInk, SessionValues, CrAgendService, $rootScope, SevenMessage, $ionicModal, flujosservice) {
        $scope.$parent.btnSave = false;
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = false;
        $scope.$parent.setExpanded(false);
        $scope.$parent.setHeaderFab(false);
        $scope.user = SessionValues.getUser();

        $timeout(function () {
            ionicMaterialMotion.slideUp({
                selector: '.slide-up'
            });
            ionicMaterialMotion.fadeSlideIn({
                selector: '.animate-fade-slide-in-right .card .item'
            });
            ionicMaterialMotion.fadeSlideInRight({
                startVelocity: 3000
            });
            ionicMaterialInk.displayEffect();
        }, 200);
        $scope.pendientes = 0;
        $scope.init = function () {
            flujosservice.cantidadFlujos($scope.user.Emp_Codi, $scope.user.Usu_Codi).then(function (success) {
                $scope.pendientes = success;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
    })

    .controller('FlujosCtrl', function ($scope, $timeout, ionicMaterialMotion, SevenMessage,
        ionicMaterialInk, $rootScope, flujosservice, SessionValues, $ionicModal, $ionicPopup, $state) {
        $scope.$parent.showHeader();
        $scope.isExpanded = true; //para saber si el header se expande un poco o no
        $timeout(function () {
            $scope.$parent.setExpanded(true);
        }, 300);
        $scope.Flujos = [];
        //$scope.MostrarDetalle = false;
        $scope.init = function () {
            flujosservice.getFlujos(SessionValues.getUser().Emp_Codi, SessionValues.getUser().Usu_Codi).then(function (response) {
                if (response.length > 0) {
                    $scope.Flujos = response;
                }
                $timeout(function () {
                    ionicMaterialInk.displayEffect();
                    ionicMaterialMotion.fadeSlideIn({
                        selector: '.animate-fade-slide-in-right .item'
                    });
                }, 100);
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err);
            });
        }

        /*Integrado*/
        $ionicModal.fromTemplateUrl('templates/acciones.html', {
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function (modal) {
            $scope.modalAcciones = modal;
        });
        $scope.showModalAccion = function () {
            $scope.modalAcciones.show();
        };
        $scope.closeModalAccion = function () {
            $scope.modalAcciones.hide();
        };
        $scope.$on('$destroy', function () {
            $scope.modalAcciones.remove();
        });
        $scope.Acciones = [];
        $scope.message_accion = "";
        $scope.ToggleDetalle = function (fluj) {
            if (SessionValues.flujoSeleccionado != null)
                SessionValues.flujoSeleccionado = !SessionValues.flujoSeleccionado.MOSTRARDETALLE;
            fluj.MOSTRARDETALLE = !fluj.MOSTRARDETALLE;
            SessionValues.flujoSeleccionado = fluj;
            //MostrarDetalle = !MostrarDetalle;
        }
        $scope.aprobarFlujo = function (flujo) {
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Desea aprobar el flujo?'
            }).then(function (res) {
                if (res) {
                    flujo.USU_CODI = SessionValues.getUser().Usu_Codi;
                    /*verificar si es necesario solicitar una acción*/
                    if (flujo.ACCIONES != null && flujo.ACCIONES.length > 0) {
                        $scope.flujoSeleccionado = flujo;
                        $scope.Acciones = flujo.ACCIONES;
                        $scope.showModalAccion();
                        return;
                    } else {
                        flujo.$aprobar().then(function (success, postResponseHeaders) {
                            if (success.State) {
                                SevenMessage.Show('El item seleccionado se aprobó exitosamente');
                            } else {
                                SevenMessage.Show('No se pudo aprobar el item seleccionado: ' + success.Message);
                            }
                            $state.go("crm.flujos", {}, {
                                reload: true
                            });
                        }, function (err) {
                            SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
                        });
                    }
                }
            });
        }
        $scope.rechazarFlujo = function (flujo) {
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Desea rechazar el flujo?'
            }).then(function (res) {
                if (res) {
                    flujo.USU_CODI = SessionValues.getUser().Usu_Codi;
                    flujo.$rechazar().then(function (success) {
                        if (success.State) {
                            SevenMessage.Show('El item seleccionado se rechazó exitosamente');
                        } else {
                            SevenMessage.Show('No se pudo rechazar el item seleccionado: ' + success.Message);
                        }
                        $state.go("crm.flujos", {}, {
                            reload: true
                        });
                    }, function (err) {
                        SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
                    });
                }
            }, function (err) {
            });
        }
        $scope.cancelarAccion = function () {
            $scope.closeModalAccion();
        }
        $scope.aceptarAccion = function (accion) {
            $scope.flujoSeleccionado.ACC_CONT = accion;
            $scope.flujoSeleccionado.$aprobar().then(function (success) {
                if (success.State) {
                    SevenMessage.Show('El item seleccionado se aprobó exitosamente');
                } else {
                    SevenMessage.Show('No se pudo aprobar el item seleccionado: ' + success.Message);
                }
                $state.go("crm.flujos", {}, {
                    reload: true
                });
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
            });
        }
    })

    .controller('DetalleFlujoCtrl', function ($scope, $timeout, ionicMaterialMotion, $state, $ionicModal,
            ionicMaterialInk, $rootScope, $ionicPopup, flujosservice, SessionValues, SevenMessage, Utils) {
        $scope.$parent.btnSave = false;
        $scope.$parent.showHeader();
        $scope.isExpanded = true; //para saber si el header se expande un poco o no
        $timeout(function () {
            $scope.$parent.setExpanded(true);
        }, 300);


        $ionicModal.fromTemplateUrl('templates/acciones.html', {
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function (modal) {
            $scope.modalAcciones = modal;
            $scope.init();
        });
        $scope.showModalAccion = function () {
            $scope.modalAcciones.show();
        };
        $scope.closeModalAccion = function () {
            $scope.modalAcciones.hide();
        };
        $scope.$on('$destroy', function () {
            $scope.modalAcciones.remove();
        });
        $scope.flujo = {};
        $scope.Acciones = [];
        $scope.message_accion = "";
        $scope.init = function () {
            var flujo = SessionValues.flujoSeleccionado;
            flujosservice.FlujosDetalle(flujo.EMP_CODI, flujo.CAS_CONT, SessionValues.getUser().Usu_Codi, flujo.SEG_CONT)
                .get().$promise.then(function (res) {
                    $scope.flujo = res;
                    $timeout(function () {
                        ionicMaterialInk.displayEffect();
                        ionicMaterialMotion.fadeSlideIn({
                            selector: '.animate-fade-slide-in-right .item'
                        });
                    }, 200);
                }, function (err) {
                    SevenMessage.Show(err);
                });
        }
        $scope.aprobarFlujo = function () {
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Desea aprobar el flujo?'
            }).then(function (res) {
                if (res) {
                    //$scope.flujo.ObjResult.SEG_COME = $scope.flujo.ObjResult.COMENTARIOS;
                    $scope.flujo.ObjResult.USU_CODI = SessionValues.getUser().Usu_Codi;
                    $scope.flujo.$aprobar().then(function (success, postResponseHeaders) {
                        if (success.State) {
                            SevenMessage.Show('El item seleccionado se aprobó exitosamente');
                        } else if (success.ObjResultAux != null) {
                            $scope.Acciones = success.ObjResultAux;
                            $scope.showModalAccion();
                            return;
                        } else {
                            SevenMessage.Show('No se pudo aprobar el item seleccionado: ' + success.Message);
                        }
                        $state.go("crm.flujos", {}, {
                            reload: true
                        });
                    }, function (err) {
                        SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
                    });
                }
            });
        }
        $scope.rechazarFlujo = function () {
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Desea rechazar el flujo?'
            }).then(function (res) {
                if (res) {
                    //$scope.flujo.ObjResult.SEG_COME = $scope.flujo.ObjResult.COMENTARIOS;
                    $scope.flujo.ObjResult.USU_CODI = SessionValues.getUser().Usu_Codi;
                    $scope.flujo.$rechazar().then(function (success) {
                        if (success.State) {
                            SevenMessage.Show('El item seleccionado se rechazó exitosamente');
                        } else {
                            SevenMessage.Show('No se pudo rechazar el item seleccionado: ' + success.Message);
                        }
                        $state.go("crm.flujos", {}, {
                            reload: true
                        });
                    }, function (err) {
                        SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
                    });
                }
            }, function (err) {
            });
        }
        $scope.cancelarAccion = function () {
            $scope.closeModalAccion();
        }
        $scope.aceptarAccion = function (accion) {
            $scope.flujo.ObjResult.ACC_CONT = accion;
            $scope.flujo.$aprobar().then(function (success) {
                if (success.State) {
                    SevenMessage.Show('El item seleccionado se aprobó exitosamente');
                } else {
                    SevenMessage.Show('No se pudo aprobar el item seleccionado: ' + success.Message);
                }
                $state.go("crm.flujos", {}, {
                    reload: true
                });
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.data.Message);
            });
        }
    })

    .controller('FriendsCtrl', function ($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion) {
        // Set Header
        $scope.$parent.btnSave = false;
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.$parent.setHeaderFab('left');

        // Delay expansion
        $timeout(function () {
            $scope.isExpanded = true;
            $scope.$parent.setExpanded(true);
        }, 300);

        // Set Motion
        ionicMaterialMotion.fadeSlideInRight();

        // Set Ink
        ionicMaterialInk.displayEffect();
    })


    .controller('actividadesCtrl', function ($scope, $stateParams, $timeout, ionicMaterialMotion,
        ionicMaterialInk, SessionValues, CrAgendService, $rootScope, SevenMessage, $ionicModal,
        $ionicPopup, actividadSrv) {
        $scope.$parent.btnSave = false;
        $ionicModal.fromTemplateUrl('templates/detalleHorario.html', {
            scope: $scope,
            animation: 'slide-in-up'
        }).then(function (modal) {
            $scope.modalHorario = modal;
        });
        $scope.showModalHorario = function () {
            $scope.modalHorario.show();
        };
        $scope.closeModalHorario = function () {
            $scope.modalHorario.hide();
        };
        $scope.$on('$destroy', function () {
            $scope.modalHorario.remove();
        });



        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
        $scope.$parent.setHeaderFab('right');

        $timeout(function () {
            ionicMaterialMotion.fadeSlideIn({
                selector: '.animate-fade-slide-in .item'
            });
        }, 200);
        ionicMaterialInk.displayEffect();

        $scope.usuario = SessionValues.getUser();
        $scope.Agenda = [];
        $scope.data = {
            usuario: SessionValues.getUser(),
            invitadoSeleccionado: '',
            Empleados: []
        }
        $scope.usuario = SessionValues.getUser();
        $scope.horarioSeleccionado = {};
        var months = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

        function getFirstDay(d) {
            d = new Date(d);
            var day = d.getDay(),
                diff = d.getDate() - day;// + (day == 0 ? -6 : 1); // adjust when day is sunday
            return new Date(d.setDate(diff));
        }
        function getLastDay(d) {
            d = new Date(d);
            var day = d.getDay(),
                diff = d.getDate() + (6 - day);// + (day == 0 ? -6 : 1); // adjust when day is sunday
            return new Date(d.setDate(diff));
        }
        $scope.setDateRange = function () {
            var fini = $scope.fini;
            var fina = $scope.fina;
            var strRangoFecha = fini.getDate() + "/" + months[fini.getMonth()] + "/" + fini.getFullYear();
            strRangoFecha += " - " + fina.getDate() + "/" + months[fina.getMonth()] + "/" + fina.getFullYear();
            document.getElementById('fechaAgenda').innerHTML = strRangoFecha;

            /*consultamos las agendas*/
            CrAgendService.getAgenda(SessionValues.getUser().Usu_Codi, fini.toDateString(), fina.toDateString()).then(function (res) {
                if (res.State) {
                    $scope.Agenda = res.ObjResult;
                    $timeout(function () {
                        ionicMaterialMotion.slideUp({
                            selector: '.slide-up'
                        });
                        ionicMaterialMotion.fadeSlideInRight({
                            startVelocity: 3000
                        });
                        ionicMaterialInk.displayEffect();
                    }, 300);
                } else {
                    SevenMessage.Show('Ocurrió un error: ' + res.Message);
                }
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.data.MessageDetail);
            });
        }
        $scope.init = function () {
            var fini = getFirstDay(new Date());
            var fina = getLastDay(new Date());
            $scope.fini = fini;
            $scope.fina = fina;
            $scope.setDateRange();
        }
        $scope.next = function () {
            var ini = new Date($scope.fina);

            var fini = new Date(ini.setDate(ini.getDate() + 1));
            var fina = new Date(ini.setDate(ini.getDate() + 6));
            $scope.fini = fini;
            $scope.fina = fina;
            $scope.setDateRange();
        }
        $scope.prev = function () {
            var ini = new Date($scope.fini);
            var ina = new Date($scope.fina);
            var fini = new Date(ini.setDate(ini.getDate() - 7));
            var fina = new Date(ina.setDate(ina.getDate() - 7));
            $scope.fini = fini;
            $scope.fina = fina;
            $scope.setDateRange();
        }
        var isCancel = false;
        $scope.seleccionarHorario = function (horario) {
            //if (!isCancel) {
            $scope.horarioSeleccionado = horario;
            $scope.showModalHorario();
            //}
            //isCancel = false;
        }
        $scope.invitarActividad = function (actividad) {
            actividadSrv.cargarEmpleados($scope.data.usuario.Usu_Codi).then(function (success) {
                $scope.data.Empleados = success;
                var popUpInvitar = $ionicPopup.show({
                    template: '<select class="item item-input item-select" style="width: 100%; max-width: 100%;" ng-model="data.invitadoSeleccionado" ng-options="emp.Usu_Codi as emp.Usu_Nomb for emp in data.Empleados"><option value="" hidden="hidden"></option></select>',
                    title: 'Seleccione la Persona a Invitar',
                    //subTitle: 'Please use normal things',
                    scope: $scope,
                    buttons: [
                      { text: 'Cancelar' },
                      {
                          text: '<b>OK</b>',
                          type: 'button-positive',
                          onTap: function (e) {
                              e.preventDefault();
                              actividadSrv.invitarActividad({
                                  Emp_Codi: actividad.EMP_CODI,
                                  Pro_Cont: actividad.PRO_CONT,
                                  Act_Codi: actividad.ACT_CODI,
                                  Usu_Codi: $scope.data.usuario.Usu_Codi,
                                  Age_Fech: actividad.AGE_FINI
                              }).then(function (success) {
                                  if (success.State) {
                                      //$state.go("crm.flujos", {}, {
                                      //    reload: true
                                      //});
                                      popUpInvitar.close();
                                      $scope.closeModalHorario();
                                      $scope.init();
                                  } else {
                                      SevenMessage.Show('Ocurrió un error: ' + success.Message);
                                  }
                              }, function (err) {
                                  SevenMessage.Show('Ocurrió un error: ' + err.Message);
                              });
                          }
                      }
                    ]
                });
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
        $scope.cancelarActividad = function (actividad) {
            isCancel = true;
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Seguro que desea cancelar la actividad?'
                //,
                //buttons: [
                //  { text: 'No' },
                //  { text: 'Si' }
                //]
            }).then(function (res) {
                if (res) {
                    CrAgendService.cancelarActividad(
                            actividad.EMP_CODI,
                            actividad.PRO_CONT,
                            actividad.ACT_CODI,
                            actividad.USU_EJEC,
                            actividad.USU_PLAN,
                            actividad.AGE_FINI,
                            $scope.usuario.Usu_Codi
                        ).then(function (success) {
                            SevenMessage.Show('Actividad cancelada correctamente.');
                        }, function (err) {
                            SevenMessage.Show('Ocurrió un error: ' + err.ExceptionMessage);
                        });
                }
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }

    })

    .controller('CrearActividadCtrl', function ($scope, $stateParams, $timeout, ionicMaterialMotion, ionicMaterialInk,
        SessionValues, actividadSrv, SevenMessage, ionicDatePicker, $cordovaDatePicker, $ionicPlatform, $filter,
        $ionicPopup, $state) {
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = true;
        $scope.$parent.btnSave = true;
        $scope.$parent.setExpanded(true);
        $timeout(function () {
            ionicMaterialMotion.fadeSlideIn({
                selector: '.animate-fade-slide-in-right .item'
            });
            ionicMaterialMotion.fadeSlideInRight({
                startVelocity: 3000
            });
            ionicMaterialInk.displayEffect();
        }, 200);
        var picker = {
            callback: function (val) {  //Mandatory
                console.log('Return value from the datepicker popup is : ' + val, new Date(val));
                alert(val);
            }
        };
        var usuario = SessionValues.getUser();
        var optionsDate = {
            date: new Date(),
            mode: 'date', // or 'time'
            minDate: new Date() - 10000,
            allowOldDates: true,
            allowFutureDates: false,
            doneButtonLabel: 'OK',
            doneButtonColor: '#F2F3F4',
            cancelButtonLabel: 'CANCELAR',
            cancelButtonColor: '#000000'
        };
        var optionsTime = {
            date: new Date(),
            mode: 'time', // or 'time'
            minDate: new Date() - 10000,
            allowOldDates: true,
            allowFutureDates: false,
            doneButtonLabel: 'OK',
            doneButtonColor: '#F2F3F4',
            cancelButtonLabel: 'CANCELAR',
            cancelButtonColor: '#000000'
        };
        $scope.showDatePicker = function () {
            $cordovaDatePicker.show(optionsDate).then(function (date) {
                //alert(date);
                $scope.data.FechaActividad = $filter('date')(date, 'yyyy-MM-dd');
            });
        };
        $scope.showTimePicker = function () {
            $cordovaDatePicker.show(optionsTime).then(function (date) {
                //alert(date);
                $scope.data.HoraActividad = $filter('date')(date, 'hh:mm a');
            });
        };
        $scope.$parent.clickSave = function () {
            //console.log($scope.data);
            if ($scope.data.pro_cont === "" || $scope.data.dpr_codi === "" || $scope.data.con_codi === "" || $scope.data.Asunto === ""
                || $scope.data.pro_cont === "-1" || $scope.data.dpr_codi === "-1" || $scope.data.con_codi === "-1") {
                SevenMessage.Show('Por favor llene todos los datos.');
                return;
            }
            $ionicPopup.confirm({
                title: 'Seven CRM',
                template: '¿Seguro que desea crear la actividad?'
            }).then(function (res) {
                if (res) {
                    actividadSrv.crearActividad({
                        Usu_Codi: usuario.Usu_Codi,
                        Emp_codi: usuario.Emp_Codi,
                        Act_Codi: $scope.data.act_codi,
                        Age_Asun: $scope.data.Asunto,
                        Pro_Cont: $scope.data.pro_cont,
                        Eta_Codi: $scope.data.eta_codi,
                        Dpr_Codi: $scope.data.dpr_codi,
                        Con_Codi: $scope.data.con_codi,
                        Age_Fech: $scope.data.FechaActividad,
                        Age_Tiem: $scope.data.HoraActividad,
                        Age_Dura: $scope.data.Duracion,
                        Usu_Ejec: $scope.data.usu_ejec
                    }).then(function (success) {
                        if (success.State) {
                            SevenMessage.Show('Actividad creada correctamente.');
                            $state.go("crm.actividades", {}, {
                                reload: true
                            });
                        } else {
                            SevenMessage.Show('Ocurrió un error: ' + success.Message);
                        }
                    }, function (err) {
                        SevenMessage.Show('Ocurrió un error: ' + err.ExceptionMessage);
                    });
                }
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }

        $scope.data = {
            filter: "",
            pro_cont: "",
            dpr_codi: "",
            con_codi: "",
            Asunto: "",
            eta_codi: "",
            act_codi: "",
            usu_ejec: "",
            FechaActividad: $filter('date')(new Date(), 'yyyy-MM-dd'),
            HoraActividad: $filter('date')(new Date(), 'hh:mm a'),
            Duracion: 1,

            Empleados: [],
            Actividades: [],
            Clientes: [],
            DClientes: [],
            Contactos: [],
            Etapas: []
        };

        $scope.init = function () {
            //ionicDatePicker.openDatePicker(picker);
            actividadSrv.cargarEmpleados(usuario.Usu_Codi).then(function (success) {
                $scope.data.Empleados = success;
                $scope.data.usu_ejec = success[0].Usu_Codi;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
            actividadSrv.cargarEtapas(usuario.Emp_Codi).then(function (success) {
                $scope.data.Etapas = success;
                $scope.data.eta_codi = success[0].ETA_CODI;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
            actividadSrv.cargarListaActividad(usuario.Emp_Codi).then(function (success) {
                $scope.data.Actividades = success;
                $scope.data.act_codi = success[0].ACT_CODI;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
        $scope.filtrarClientes = function () {
            actividadSrv.cargarClientes(usuario.Emp_Codi, $scope.data.filter).then(function (success) {
                //console.log(success);
                $scope.data.Clientes = success.ObjResult;
                $scope.data.DClientes = success.ObjResultAux;
                $scope.data.Contactos = success.ObjResultAux2;

                $scope.data.pro_cont = success.ObjResult[0].PRO_CONT;
                $scope.data.dpr_codi = success.ObjResultAux[0].DPR_CODI;
                $scope.data.con_codi = success.ObjResultAux2[0].CON_CODI;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
        $scope.changeCliente = function (pro_cont) {
            actividadSrv.cargarDClientes(pro_cont).then(function (success) {
                $scope.data.DClientes = success.ObjResult;
                $scope.data.Contactos = success.ObjResultAux;

                $scope.data.dpr_codi = success.ObjResult[0].DPR_CODI;
                $scope.data.con_codi = success.ObjResultAux[0].CON_CODI;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
        $scope.changeDCliente = function (dpr_codi) {
            actividadSrv.cargarContactos($scope.data.pro_cont, dpr_codi).then(function (success) {
                $scope.data.Contactos = success.ObjResult;
                $scope.data.con_codi = success.ObjResult[0].CON_CODI;
            }, function (err) {
                SevenMessage.Show('Ocurrió un error: ' + err.Message);
            });
        }
    })

    .controller('ActivityCtrl', function ($scope, $stateParams, $timeout, ionicMaterialMotion, ionicMaterialInk) {
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
        $scope.$parent.setHeaderFab('right');

        $timeout(function () {
            ionicMaterialMotion.fadeSlideIn({
                selector: '.animate-fade-slide-in .item'
            });
        }, 200);

        // Activate ink for controller
        ionicMaterialInk.displayEffect();
    })

    .controller('GalleryCtrl', function ($scope, $stateParams, $timeout, ionicMaterialInk, ionicMaterialMotion) {
        $scope.$parent.showHeader();
        $scope.$parent.clearFabs();
        $scope.isExpanded = true;
        $scope.$parent.setExpanded(true);
        $scope.$parent.setHeaderFab(false);

        // Activate ink for controller
        ionicMaterialInk.displayEffect();

        ionicMaterialMotion.pushDown({
            selector: '.push-down'
        });
        ionicMaterialMotion.fadeSlideInRight({
            selector: '.animate-fade-slide-in .item'
        });
    })

;
