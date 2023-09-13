import { mapEnumToOptions } from '@abp/ng.core';

export enum ViajeType {
  Colectivo = 0,
  Avion = 1,
  Automovil = 2,
}

export const viajeTypeOptions = mapEnumToOptions(ViajeType);
