import { useEffect, useRef } from 'react';
import styles from './MatrixRain.module.css';


const letters = ['W', 'E', 'L', 'C', 'O', 'M', 'E', ' ', 'T', 'O', ' ', 'O', 'C', 'R'];

const MatrixRain: React.FC = () => {
  const canvasRef1 = useRef<HTMLCanvasElement | null>(null);
  const canvasRef2 = useRef<HTMLCanvasElement | null>(null);
  const spansRef = useRef<(HTMLSpanElement | null)[]>([]);

  useEffect(() => {
    const canvas = canvasRef1.current;
    const canvas2 = canvasRef2.current;
    if (!canvas || !canvas2) return;

    const ctx = canvas.getContext('2d')!;
    const ctx2 = canvas2.getContext('2d')!;

    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    canvas2.width = window.innerWidth;
    canvas2.height = window.innerHeight;

   class Symbol {
  private characters: string;
  public text: string;
  public x: number;
  public y: number;
  public fontSize: number;
  public canvasHeight: number;

  constructor(x: number, y: number, fontSize: number, canvasHeight: number) {
    this.x = x;
    this.y = y;
    this.fontSize = fontSize;
    this.canvasHeight = canvasHeight;
    this.characters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    this.text = 'A';
  }

  draw(context: CanvasRenderingContext2D, context2: CanvasRenderingContext2D) {
    this.text = this.characters.charAt(Math.floor(Math.random() * this.characters.length));
    const xPos = this.x * this.fontSize;
    const yPos = this.y * this.fontSize;
    context.fillText(this.text, xPos, yPos);
    context2.fillText(this.text, xPos, yPos);

    if (yPos > this.canvasHeight && Math.random() > 0.97) {
      this.y = 0;
    } else {
      this.y += 0.9;
    }
  }
}

    class Effect {
  public fontSize: number;
  public columns: number;
  public symbols: Symbol[] = [];
  public canvasWidth: number;
  public canvasHeight: number;

  constructor(canvasWidth: number, canvasHeight: number) {
    this.canvasWidth = canvasWidth;
    this.canvasHeight = canvasHeight;
    this.fontSize = 16;
    this.columns = Math.floor(this.canvasWidth / this.fontSize);
    this.initialize();
  }

  initialize() {
    for (let i = 0; i < this.columns; i++) {
      this.symbols[i] = new Symbol(i, 0, this.fontSize, this.canvasHeight);
    }
  }

  resize(width: number, height: number) {
    this.canvasWidth = width;
    this.canvasHeight = height;
    this.columns = Math.floor(this.canvasWidth / this.fontSize);
    this.symbols = [];
    this.initialize();
  }
}

    const effect = new Effect(canvas.width, canvas.height);
    let lastTime = 0;
    const fps = 26;
    const nextFrame = 1000 / fps;
    let timer = 0;

    const animate = (timeStamp: number) => {
      const deltaTime = timeStamp - lastTime;
      lastTime = timeStamp;

      if (timer > nextFrame) {
        ctx.textAlign = 'center';
        ctx.fillStyle = 'rgba(22, 26, 29, 0.05)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.font = `${effect.fontSize}px monospace`;
        ctx.fillStyle = '#0369A1';

        ctx2.textAlign = 'center';
        ctx2.clearRect(0, 0, canvas.width, canvas.height);
        ctx2.font = `${effect.fontSize}px monospace`;
        ctx2.fillStyle = '#0369A1';

        effect.symbols.forEach((symbol) => symbol.draw(ctx, ctx2));
        timer = 0;
      } else {
        timer += deltaTime;
      }

      requestAnimationFrame(animate);
    };

    animate(0);

    const handleResize = () => {
      canvas.width = window.innerWidth;
      canvas.height = window.innerHeight;
      canvas2.width = window.innerWidth;
      canvas2.height = window.innerHeight;
      effect.resize(canvas.width, canvas.height);
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  useEffect(() => {
    let dataCounter = 0;
    const spans = spansRef.current;

    spans.forEach((span) => {
      if (span) {
        const changeLimit = Math.floor(Math.random() * 100);
        span.dataset.change = changeLimit.toString();
      }
    });

    const updateLetters = () => {
      const index = Math.floor(Math.random() * spans.length);
      const span = spans[index];
      if (!span || !span.classList.contains('str')) return;

      span.innerText = Math.floor(Math.random() * 10).toString();
      span.dataset.number = (dataCounter++).toString();

      spans.forEach((el, i) => {
        if (!el) return;
        if (parseInt(el.dataset.number || '0') > parseInt(el.dataset.change || '100')) {
          el.innerText = letters[i];
          el.classList.remove('str');
        }
      });
    };

    const interval = setInterval(updateLetters, 30);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className={styles.wrapper}>
      <canvas ref={canvasRef1} />
      <canvas ref={canvasRef2} />
      <div className={styles.overlayText}>
        {letters.map((char, index) => (
          <span
            key={index}
            ref={(el) => {
              spansRef.current[index] = el;
            }}
            className="str"
            data-change="0"
          >
            0
          </span>
        ))}
      </div>
    </div>
  );
};

export default MatrixRain;
