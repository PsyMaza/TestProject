import { Observable } from 'rxjs';


export abstract class BaseService {

    constructor() { }

    protected handleError(error: any) {
        let applicationError = error.headers.get('Application-Error');

        if (applicationError) {
            return Observable.throw(applicationError);
        }

        let modelStateErrors = '';
        let serverError = error.json();

        if (!serverError.type) {
            for (let key in serverError) {
                if (serverError[key].type) {
                    // tslint:disable-next-line:forin
                    for (let error in serverError[key]) {
                        modelStateErrors += serverError[key][error] + '\n';
                        }
                } else {
                    modelStateErrors += serverError[key] + '\n';
                }
            }
        }

        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
        return Observable.throw(modelStateErrors || 'Server error');
    }
}
