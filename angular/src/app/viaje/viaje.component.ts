import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ViajeService, ViajeDto} from '@proxy/viajes';

@Component({
  selector: 'app-viaje',
  templateUrl: './viaje.component.html',
  styleUrls: ['./viaje.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class ViajeComponent implements OnInit {
  viaje = { items: [], totalCount: 0 } as PagedResultDto<ViajeDto>;

  isModalOpen = false;

  form: FormGroup;
  minDate: NgbDateStruct;
  maxDate: NgbDateStruct;
  selectedViaje = {} as ViajeDto;
  filterDateExit: NgbDateStruct | string | null = null;

  constructor(
    public readonly list: ListService,
    private viajeService: ViajeService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) { 
    const dateCurrent  = new Date();
    const formatCurrent = dateCurrent.toISOString().slice(0, 19).replace("T", " ");
    this.filterDateExit = formatCurrent;
  }

  ngOnInit(): void {
    const viajeStreamCreator = (query) => this.viajeService.getList(query);
    this.list.hookToQuery(viajeStreamCreator).subscribe((response) => {
      this.viaje = response;    
    });

  }

  aplicarFiltro() {

    if (this.filterDateExit) {
        const dateFormat = this.formatDateToCustomFormat(this.filterDateExit);
        console.log("formato nuevo", dateFormat);
        const viajeStreamCreator = (query) => {
          query.filter= dateFormat
          return this.viajeService.getList(query)
        };
        this.list.hookToQuery(viajeStreamCreator).subscribe((response) => {
          this.viaje = response;
        });
    }
  
  }

  formatDateToCustomFormat(date) {
    const fechaOriginal = new Date(date);

    const dia = fechaOriginal.getDate().toString();
    const mes = (fechaOriginal.getMonth() + 1).toString(); 
    const año = fechaOriginal.getFullYear();
    const horas = fechaOriginal.getHours().toString().padStart(2, "0");
    const minutos = fechaOriginal.getMinutes().toString().padStart(2, "0");
    const segundos = fechaOriginal.getSeconds().toString().padStart(2, "0");
    const formatoPersonalizado = `${dia}/${mes}/${año} ${horas}:${minutos}:${segundos}`;
    return formatoPersonalizado;
}

  createViaje() {
    this.selectedViaje = {} as ViajeDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editViaje(id: string) {
    this.viajeService.get(id).subscribe((viaje) => {
      this.selectedViaje = viaje;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      fecha_de_salida: [this.selectedViaje.fecha_de_salida ? new Date(this.selectedViaje.fecha_de_salida) : '', Validators.required],
      fecha_de_llegada: [this.selectedViaje.fecha_de_llegada ? new Date(this.selectedViaje.fecha_de_llegada) :'', Validators.required],
      origen: [this.selectedViaje.origen || '', Validators.required],
      destino: [this.selectedViaje.destino || '', Validators.required],
      medio_transporte: [Number(this.selectedViaje.destino) || 0,Validators.required],    
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedViaje.id) {
      this.viajeService
        .update(this.selectedViaje.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.viajeService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  deleteViaje(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.viajeService.delete(id).subscribe(() => this.list.get());
        }
      });
  }
}