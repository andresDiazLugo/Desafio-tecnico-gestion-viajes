import type { CreateReservaDto, GetReservaListDto, PasajeroLookupDto, ReservaDto, UpdateReservaDto, ViajeLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReservaService {
  apiName = 'Default';
  

  create = (input: CreateReservaDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ReservaDto>({
      method: 'POST',
      url: '/api/app/reserva',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/reserva/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ReservaDto>({
      method: 'GET',
      url: `/api/app/reserva/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetReservaListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ReservaDto>>({
      method: 'GET',
      url: '/api/app/reserva',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getPasajeroLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<PasajeroLookupDto>>({
      method: 'GET',
      url: '/api/app/reserva/pasajero-lookup',
    },
    { apiName: this.apiName,...config });
  

  getViajeLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<ViajeLookupDto>>({
      method: 'GET',
      url: '/api/app/reserva/viaje-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateReservaDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/reserva/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
