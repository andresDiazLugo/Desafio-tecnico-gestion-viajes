import type { CreatePasajeroDto, GetPasajeroListDto, PasajeroDto, UpdatePasajeroDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PasajeroService {
  apiName = 'Default';
  

  create = (input: CreatePasajeroDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PasajeroDto>({
      method: 'POST',
      url: '/api/app/pasajero',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/pasajero/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PasajeroDto>({
      method: 'GET',
      url: `/api/app/pasajero/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetPasajeroListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PasajeroDto>>({
      method: 'GET',
      url: '/api/app/pasajero',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdatePasajeroDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/pasajero/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
