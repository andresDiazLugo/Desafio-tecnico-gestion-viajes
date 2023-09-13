import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ReservaRoutingModule } from './reserva-routing.module';
import { ReservaComponent } from './reserva.component';
import { NgbDatepickerModule} from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    ReservaComponent
  ],
  imports: [
    SharedModule,
    ReservaRoutingModule,
    NgbDatepickerModule
  ]
})
export class ReservaModule { }
