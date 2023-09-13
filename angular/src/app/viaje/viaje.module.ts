import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ViajeRoutingModule } from './viaje-routing.module';
import { ViajeComponent } from './viaje.component';
import { NgbDatepickerModule} from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    ViajeComponent
  ],
  imports: [
    SharedModule,
    ViajeRoutingModule,
    NgbDatepickerModule
  ]
})
export class ViajeModule { }
