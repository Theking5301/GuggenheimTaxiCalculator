import { Directive, ElementRef, HostListener, Renderer2, Output, EventEmitter } from '@angular/core';

@Directive({
	selector: '[TimeValidator]'
})
export class TimeValidatorDirective {
	@Output()
	private validityChanged: EventEmitter<boolean> = new EventEmitter();
	private TimeRegex: RegExp;
	constructor(private _ElementRef: ElementRef, private _Renderer: Renderer2) {
		this.TimeRegex = /^[0-9][0-9]:[0-5][0-9]$/;
	}

	@HostListener('change') onValueChanged() {
		let result = this._ElementRef.nativeElement.value.match(this.TimeRegex);
		if (!this._ElementRef.nativeElement.value.match(this.TimeRegex)) {
			this._Renderer.addClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(false);
		} else {
			this._Renderer.removeClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(true);
		}
	}
}
