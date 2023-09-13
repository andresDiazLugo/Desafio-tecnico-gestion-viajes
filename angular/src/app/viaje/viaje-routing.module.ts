import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViajeComponent } from './viaje.component';

const routes: Routes = [{ path: '', component: ViajeComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ViajeRoutingModule { }
