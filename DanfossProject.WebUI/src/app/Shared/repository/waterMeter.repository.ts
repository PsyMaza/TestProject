import { Injectable } from '@angular/core';
import { ConfigService } from '../util/config.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { WaterMeterCreateModel } from '../models/waterMeter-create.model';
import { WaterMeter } from '../models/waterMeter.model';
import { WaterMeterUpdateModel } from '../models/waterMeter-update.model';

@Injectable()
export class WaterMeterRepository {

    baseUrl = '';
    headers: HttpHeaders = null;

    private waterMeters: WaterMeter[] = [];

    constructor (
        private http: HttpClient,
        private configService: ConfigService
    ) {
        this.baseUrl = configService.getApiURI();
        this.headers = configService.getHeaders();
    }

    getWaterMeters() {
        return this.http.get<WaterMeter[]>(this.baseUrl + 'waterMeters/getall', { headers: this.headers });
    }

    getWaterMeter(id: number) {
        return this.http.get<WaterMeter>(this.baseUrl + `waterMeters/getbyid/${id}`, { headers: this.headers });
    }

    addWaterMeter(waterMeter: WaterMeterCreateModel) {
        const body = JSON.stringify(waterMeter);

        return this.http.post(this.baseUrl + 'waterMeters/add', body, { headers: this.headers});
    }

    updateWaterMeter(id, waterMeter: WaterMeterUpdateModel) {
        const body = JSON.stringify(waterMeter);

        return this.http.put(this.baseUrl + `watermeters/UpdateById/${id}`, body, { headers: this.headers});
    }

    deleteWaterMeter(id: number) {
        return this.http.delete(this.baseUrl + `waterMeters/delete/${id}`, {headers: this.headers});
    }
}
