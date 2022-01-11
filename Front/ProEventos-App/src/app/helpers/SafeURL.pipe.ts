import { Pipe, PipeTransform } from '@angular/core';
import {
  DomSanitizer,
  SafeHtml,
  SafeResourceUrl,
  SafeScript,
  SafeStyle,
  SafeUrl,
} from '@angular/platform-browser';

@Pipe({
  name: 'SafeURL',
})
export class SafeURLPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(value: any, args?: any): any {
    return this.sanitizer.bypassSecurityTrustUrl(value);
  }

  /**
      //  * Pipe Constructor
      //  *
      //  */
  // // tslint:disable-next-line
  // constructor(protected _sanitizer: DomSanitizer) {
  // }

  // /**
  //  * Transform
  //  *
  //  * @param value: string
  //  * @param type: string
  //  */
  // transform(value: string, type: string): SafeHtml | SafeStyle | SafeScript | SafeUrl | SafeResourceUrl {
  //   switch (type) {
  //     case 'html':
  //       return this._sanitizer.bypassSecurityTrustHtml(value);
  //     case 'style':
  //       return this._sanitizer.bypassSecurityTrustStyle(value);
  //     case 'script':
  //       return this._sanitizer.bypassSecurityTrustScript(value);
  //     case 'url':
  //       return this._sanitizer.bypassSecurityTrustUrl(value);
  //     case 'resourceUrl':
  //       return this._sanitizer.bypassSecurityTrustResourceUrl(value);
  //     default:
  //       return this._sanitizer.bypassSecurityTrustHtml(value);
  //   }
  // }
}
