\NeedsTeXFormat{LaTeX2e}
\ProvidesClass{iiuwb}[2011/11/26 Institute of Informatics of UwB LaTeX class]
\DeclareOption*{% wszystkie opcje
\PassOptionsToClass{\CurrentOption}{report}}
\ProcessOptions
\LoadClass[12pt,a4paper,twoside]{report}
\RequirePackage[T1]{fontenc}
\RequirePackage[utf8]{inputenc}
\RequirePackage{array,graphicx,ifthen,float}

%Komendy, które muszą być nadpisane przez autora
\DeclareRobustCommand{\imiona}{Imię}
\DeclareRobustCommand{\nazwiska}{Nazwisko}

\DeclareRobustCommand{\stopienpromotora}{dr}
\DeclareRobustCommand{\imionapromotora}{Promotor}
\DeclareRobustCommand{\nazwiskapromotora}{Promotor}
\DeclareRobustCommand{\profuwb}{TAK}

\DeclareRobustCommand{\stopienasystenta}{dr}
\DeclareRobustCommand{\imionaasystenta}{Asystent}
\DeclareRobustCommand{\nazwiskaasystenta}{Asystent}

\DeclareRobustCommand{\rok}{2012}
\DeclareRobustCommand{\tytul}{Tytuł}
\DeclareRobustCommand{\zaklad}{Zakład}
\DeclareRobustCommand{\album}{00000}
\DeclareRobustCommand{\rokakademicki}{2011/2012}
\DeclareRobustCommand{\kierunek}{Informatyka}
\DeclareRobustCommand{\sciezka}{Informatyka teoretyczna}
\DeclareRobustCommand{\rodzaj}{Stacjonarne}
\DeclareRobustCommand{\poziom}{I stopnia}

\DeclareRobustCommand{\stronatytulowa}{%
\begin{titlepage}\Large
\begin{center}
UNIWERSYTET W BIAŁYMSTOKU\\
WYDZIAŁ MATEMATYKI I INFORMATYKI\\
INSTYTUT INFORMATYKI
\end{center}
\vfill
\begin{center}
{\large \imiona{} \MakeUppercase{\nazwiska}}\\
\end{center}
\vfill
\begin{center}
{\LARGE \textbf{\tytul}}
\end{center}
\vfill
\begin{flushright}
\begin{tabular}{r}

Promotor:\\
\stopienpromotora\ \imionapromotora{} \MakeUppercase{\nazwiskapromotora}\ifthenelse{\equal{\profuwb}{TAK}}{, prof. UwB}{}
\\

\ifthenelse{\NOT{\equal{\nazwiskaasystenta}{}}}{
Asystent:\\
\stopienasystenta\ \imionaasystenta{} \MakeUppercase{\nazwiskaasystenta}\\
}{}
\end{tabular}
\end{flushright}
\vfill
\begin{center}
BIAŁYSTOK \rok
\end{center}
\end{titlepage}
}

\DeclareRobustCommand{\ramka}{%
\begin{titlepage}
\begin{center}
\textbf{Karta dyplomowa}
\end{center}

\vspace{1cm}

\begin{tabular}{|c|c|c|}\hline

\begin{minipage}{.3\textwidth}
\vspace{5mm}
UNIWERSYTET W~BIAŁYMSTOKU 

\vspace{1cm}

Wydział Matematyki i~Informatyki

\vspace{1cm}

Zakład / Katedra

\zaklad

\vspace{3mm}

\end{minipage}

&

\begin{minipage}{.3\textwidth}
Studia \rodzaj

\poziom

\end{minipage}

&

\begin{minipage}{.3\textwidth}
\vspace{5mm}
Nr albumu studenta

\album

\vspace{3mm}\hrule\vspace{3mm}

Rok akademicki

\rokakademicki

\vspace{3mm}\hrule\vspace{3mm}

Kierunek studiów

\kierunek

\vspace{3mm}\hrule\vspace{3mm}

Ścieżka kształcenia

\sciezka

\vspace{5mm}
\end{minipage}

\\\hline

\multicolumn{3}{|c|}{
\begin{minipage}{\textwidth}
\vspace{5mm}
\begin{center}
\begin{tabular}{c}
\makebox[.8\textwidth]{\imiona\ \nazwiska}\\[-2mm]
\dotfill\\[-2mm]
{\scriptsize Imię i nazwisko studenta}\\
\end{tabular}
\end{center}
TEMAT PRACY DYPLOMOWEJ:

\begin{center}
\tytul
\end{center}

\vspace{5mm}
\end{minipage}
}

\\\hline

\multicolumn{3}{|c|}{
\begin{minipage}{\textwidth}
\vspace{1cm}
\begin{center}
\begin{tabular}{cc}
\makebox[.4\textwidth]{\dotfill} & \makebox[.4\textwidth]{\dotfill} \\[-2mm]
{\scriptsize Ocena promotora} & {\scriptsize Podpis promotora}
\end{tabular}
\end{center}
\vspace{3mm}
\end{minipage}
}

\\\hline

\multicolumn{3}{|c|}{
\begin{minipage}{\textwidth}
\vspace{1cm}
\begin{center}
\begin{tabular}{ccc}
\makebox[.4\textwidth]{\dotfill} & \makebox[.2\textwidth]{\dotfill} & \makebox[.3\textwidth]{\dotfill} \\[-2mm]
{\scriptsize Imię i nazwisko recenzenta} & {\scriptsize Ocena recenzenta} & {\scriptsize Podpis recenzenta}
\end{tabular}
\end{center}
\vspace{3mm}
\end{minipage}
}

\\\hline
\end{tabular}
\end{titlepage}
}

\AtBeginDocument{%
\renewcommand{\baselinestretch}{1.3}
\stronatytulowa\newpage
\ramka\newpage
\setcounter{page}{1}
}

%POZIOM

\setlength{\hoffset}{-1in}
\setlength{\oddsidemargin}{35mm}
\setlength{\evensidemargin}{25mm}
\setlength{\textwidth}{150mm}
\setlength{\marginparsep}{0mm}
\setlength{\marginparwidth}{0mm}

%PION

\setlength{\voffset}{-1in}
\setlength{\topmargin}{25mm}
\setlength{\headheight}{0mm}
\setlength{\headsep}{0mm}
\setlength{\textheight}{247mm}
\setlength{\footskip}{15mm}

\endinput
