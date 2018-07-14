import { Address } from './address.interface';

export class BuildingUpdateModel {
    constructor (
        public Id: number,
        // tslint:disable-next-line:no-shadowed-variable
        public Address: Address,
        public Company: string,
        public WaterMeterId: number
    ) {}
}
