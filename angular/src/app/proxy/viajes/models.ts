import type { ViajeType } from './viaje-type.enum';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateViajeDto {
  fecha_de_salida: string;
  fecha_de_llegada: string;
  origen: string;
  destino: string;
  medio_transporte: ViajeType;
  pasajeroID: string;
}

export interface GetViajeListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateViajeDto {
  fecha_de_salida: string;
  fecha_de_llegada: string;
  origen: string;
  destino: string;
  medio_transporte: ViajeType;
}

export interface ViajeDto extends EntityDto<string> {
  fecha_de_salida?: string;
  fecha_de_llegada?: string;
  origen?: string;
  destino?: string;
  medio_transporte: ViajeType;
}
