import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreatePasajeroDto extends EntityDto<string> {
  nombre: string;
  apellido: string;
  dni: string;
  email: string;
  fecha_de_nacimiento: string;
}

export interface GetPasajeroListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface PasajeroDto extends EntityDto<string> {
  nombre?: string;
  apellido?: string;
  dni?: string;
  email?: string;
  password?: string;
  fecha_de_nacimiento?: string;
  userId?: string;
}

export interface UpdatePasajeroDto {
  nombre?: string;
  apellido?: string;
  dni?: string;
  email?: string;
  fecha_de_nacimiento?: string;
}
