import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { PasajeroDto } from '../pasajeros/models';
import type { ViajeDto } from '../viajes/models';

export interface CreateReservaDto extends EntityDto<string> {
  coordinador?: boolean;
  pasajeroId?: string;
  vIajeId?: string;
}

export interface GetReservaListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface PasajeroLookupDto extends EntityDto<string> {
  nombre?: string;
  dni?: string;
}

export interface ReservaDto extends EntityDto<string> {
  coordinador?: boolean;
  pasajeroId?: string;
  pasajero: PasajeroDto;
  vIajeId?: string;
  viaje: ViajeDto;
}

export interface UpdateReservaDto {
  coordinador?: boolean;
  pasajeroId?: string;
  viajeId?: string;
}

export interface ViajeLookupDto extends EntityDto<string> {
  origen?: string;
  destino?: string;
  fecha_de_salida?: string;
  fecha_de_llegada?: string;
}
