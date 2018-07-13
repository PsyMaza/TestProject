export class WaterMeterUpdateModel {
    constructor (
        public Id: number,
        public SerialNumber: string,
        public CounterValue: number
    ) {}
}