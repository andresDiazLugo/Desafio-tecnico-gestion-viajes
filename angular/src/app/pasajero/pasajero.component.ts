import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { PasajeroService, PasajeroDto } from '@proxy/pasajeros';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-pasajero',
  templateUrl: './pasajero.component.html',
  styleUrls: ['./pasajero.component.scss'],
  providers: [ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ]
})
export class PasajeroComponent implements OnInit {
  pasajero = {items: [], totalCount:0} as PagedResultDto<PasajeroDto>;
    
  selectedPasajero = {} as PasajeroDto; 

  form: FormGroup;

  isModalOpen = false;

  minDate: NgbDate = new NgbDate(1900, 1, 1);
  maxDate: NgbDate = new NgbDate(2028, 12, 31);


  constructor(public readonly list: ListService, private  pasajeroService: PasajeroService, private fb: FormBuilder,private confirmation: ConfirmationService,private http: HttpClient){}
  
  ngOnInit(){
    const pasajeroStreamCreator = (query) => this.pasajeroService.getList(query);
    this.list.hookToQuery(pasajeroStreamCreator).subscribe((response) => {
      console.log("esta es la respuesta",response);
      this.pasajero = response;
    })
  }

  createPassenger() {
    this.selectedPasajero = {} as PasajeroDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  deleteRegister(id: string){
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) =>{
      if (status === Confirmation.Status.confirm){
        this.pasajeroService.delete(id).subscribe(()=> this.list.get())
      }
    })
  }
  editPasajero(id:string){
    this.pasajeroService.get(id).subscribe((pasajero)=>{
      this.selectedPasajero = pasajero
      this.buildForm();
      this,this.isModalOpen = true;
    })
  }
  buildForm(){
    this.form  = this.fb.group({
      nombre:[ this.selectedPasajero.nombre || '',Validators.required],
      apellido: [ this.selectedPasajero.apellido || '', Validators.required],
      dni: [this.selectedPasajero.dni || '', [Validators.required, Validators.minLength(8)]],
      fecha_de_nacimiento: [
        this.selectedPasajero.fecha_de_nacimiento ? new Date(this.selectedPasajero.fecha_de_nacimiento) : '',Validators.required
        ],
      email:[this.selectedPasajero.email || '',Validators.required],
    })
  }

  save(){
    if (this.form.invalid){
      return ;
    }

    const dataFormValue =  this.form.value ;
  

    const request = this.selectedPasajero.id

    if (request){
      this.pasajeroService.update(this.selectedPasajero.id, this.form.value).subscribe(()=>{
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      })
    }else{
      this.pasajeroService.create(dataFormValue).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }

  }

}
