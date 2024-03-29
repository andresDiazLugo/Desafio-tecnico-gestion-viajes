import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PasajeroComponent } from './pasajero.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';

const routes: Routes = [{ path: '', component: PasajeroComponent, canActivate: [AuthGuard, PermissionGuard]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PasajeroRoutingModule { }
