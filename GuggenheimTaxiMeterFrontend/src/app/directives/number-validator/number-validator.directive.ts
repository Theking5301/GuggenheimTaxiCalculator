import { Directive, ElementRef, HostListener, Renderer2, Output, EventEmitter, Input } from '@angular/core';

@Directive({
	selector: '[NumberValidator]'
})
export class NumberValidatorDirective {
	@Output()
	private validityChanged: EventEmitter<boolean> = new EventEmitter();
	@Input()
	MinimumNumber: number;
	@Input()
	MaximumNumber: number;

	constructor(private _ElementRef: ElementRef, private _Renderer: Renderer2) {
		this.MinimumNumber = Number.MIN_SAFE_INTEGER;
		this.MaximumNumber = Number.MAX_SAFE_INTEGER;
	}

	@HostListener('change') onValueChanged() {
		let val = this._ElementRef.nativeElement.value;

		if (isNaN(val) || val > this.MaximumNumber || val < this.MinimumNumber) {
			this._Renderer.addClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(false);
		} else {
			this._Renderer.removeClass(this._ElementRef.nativeElement, 'invalid_input');
			this.validityChanged.emit(true);
		}
	}
}
