import { Address } from './address.interface';

export class BuildingCreateModel {
    constructor (
        public Address: Address,
        public Company: string,
        public WaterMeterId: number
    ) {}
}
