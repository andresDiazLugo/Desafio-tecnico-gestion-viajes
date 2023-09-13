import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { PasajeroRoutingModule } from './pasajero-routing.module';
import { PasajeroComponent } from './pasajero.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PasajeroComponent
  ],
  imports: [
    SharedModule,
    PasajeroRoutingModule,
    NgbDatepickerModule,
    FormsModule
  ]
})
export class PasajeroModule { }
