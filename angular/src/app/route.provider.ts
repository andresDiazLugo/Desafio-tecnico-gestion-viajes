import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/gestion',
        name: 'GestionViajes',
        iconClass: 'fas fa-address-card',
        order: 2,
        layout: eLayoutType.application
      },
      {
        path: '/pasajeros',
        name: '::Menu:Pasajeros',
        parentName:'GestionViajes',
        layout: eLayoutType.application,
        iconClass: 'fa fa-user',
        requiredPolicy:'EntrevistaABP.Pasajeros'
        
      },
      {
        path: '/viajes',
        name: '::Menu:Viajes',
        parentName: 'GestionViajes',
        iconClass: 'fa fa-calendar-check-o',
        layout: eLayoutType.application,
      },
      {
        path: '/reservas',
        name: '::Menu:Reservas',
        parentName: 'GestionViajes',
        iconClass: 'fa fa-plane',
        layout: eLayoutType.application,
        requiredPolicy:'EntrevistaABP.Reservas'

      },
    ]);
  };
}
