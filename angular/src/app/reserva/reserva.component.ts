import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ReservaService,ReservaDto, PasajeroLookupDto, ViajeLookupDto} from '@proxy/reservas';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'app-reserva',
  templateUrl: './reserva.component.html',
  styleUrls: ['./reserva.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class ReservaComponent  {
  reservas = { items: [], totalCount: 0 } as PagedResultDto<ReservaDto>

  isModalOpen = false;

  form: FormGroup;

  pasajeros$: Observable<PasajeroLookupDto[]>
  viajes$: Observable<ViajeLookupDto[]>
  selectedReserva = {} as ReservaDto;
  
  constructor(
    public readonly list: ListService,
    private reservaService: ReservaService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) { 
    this.pasajeros$ = reservaService.getPasajeroLookup().pipe(map((r)=>r.items));
    this.viajes$ = reservaService.getViajeLookup().pipe(map((r)=>r.items));
  }
  ngOnInit(): void {
    const reservaStreamCreator = (query) => this.reservaService.getList(query);
    this.viajes$.subscribe((viajes) => {
      console.log('Datos de viajes:', viajes);
    });
    
    this.list.hookToQuery(reservaStreamCreator).subscribe((response) => {
      this.reservas = response;
    });
  }

  createReserva() {
    this.selectedReserva = {} as ReservaDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editReserva(id: string) {
    this.reservaService.get(id).subscribe((reserva) => {
      this.selectedReserva = reserva;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      pasajeroId: [this.selectedReserva.pasajeroId || Validators.required],
      viajeId: [this.selectedReserva.vIajeId || Validators.required],
      coordinador: [this.selectedReserva.coordinador || false]
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedReserva.id) {
      this.reservaService
        .update(this.selectedReserva.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.reservaService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  deleteReserva(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.reservaService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

}
