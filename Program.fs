namespace NewApp

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.DSL
open Avalonia.FuncUI.Elmish
open Avalonia.FuncUI.Hosts
open Avalonia.FuncUI.Types
open Avalonia.Themes.Fluent
open Avalonia.Svg.Skia
open Elmish

type MainWindow() as this =
    inherit HostWindow()

    do
        base.Title <- "NewApp"
        base.Width <- 400.0
        base.Height <- 400.0

        let view _ _ : IView =
            Svg.create [ Svg.path "avaloinauinet-icon.svg" ]

        Program.mkSimple (fun _ -> ()) (fun _ _ -> ()) view
        |> Program.withHost this
        |> Program.withConsoleTrace
        |> Program.run

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add(FluentTheme(baseUri = null, Mode = FluentThemeMode.Dark))

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime -> desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main (args: string []) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)
