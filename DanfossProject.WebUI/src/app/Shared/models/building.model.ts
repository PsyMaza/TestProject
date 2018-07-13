import { Address } from './address.interface';
import { WaterMeter } from './waterMeter.model';

export class Building {
    constructor (
        public Id: number,
        public Address: Address,
        public Company: string,
        public WaterMeter: WaterMeter
    ) {}
}
