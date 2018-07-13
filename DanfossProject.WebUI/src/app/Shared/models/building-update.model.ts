import { Address } from './address.interface';

export class BuildingUpdateModel {
    constructor (
        public Id: number,
        public Address: Address,
        public Company: string,
        public WaterMeterId: number
    ) {}
}
