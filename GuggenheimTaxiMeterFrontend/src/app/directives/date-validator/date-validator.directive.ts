import { Directive, ElementRef, HostListener, Renderer2, Output, EventEmitter } from '@angular/core';

@Directive({
	selector: '[DateValidator]'
})
export class DateValidatorDirective {
	@Output() 
	private validityChanged: EventEmitter<boolean> = new EventEmitter();

	constructor(private _ElementRef: ElementRef, private _Renderer: Renderer2) { }

	@HostListener('change') onValueChanged() {
		let milliseconds = Date.parse(this._ElementRef.nativeElement.value);
		
		if (isNaN(milliseconds)) {
			this._Renderer.addClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(false);
		} else {
			this._Renderer.removeClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(true);
		}
	}
}
