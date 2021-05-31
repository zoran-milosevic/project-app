import { Injectable } from '@angular/core';

import { environment } from '@environments/environment';

@Injectable({ providedIn: 'root' })
export class LogService {

    public static ENABLE: boolean = environment.enableLogService;

    public debug(msg: any, ...formatParams: any[]) {
        if (LogService.ENABLE) {
            console.log(msg, formatParams);
        }
    }

    public error(msg: any, ...formatParams: any[]) {
        if (LogService.ENABLE) {
            console.error(msg, formatParams);
        }
    }

    public inspect(obj: any) {
        if (LogService.ENABLE) {
            console.log(obj);
            console.log('typeof: ', typeof obj);

            if (obj) {
                console.log('constructor: ', obj.constructor.name);

                for (let key in obj) {
                    console.log(`${key}: `, obj[key]);
                }
            }
        }
    }
}
