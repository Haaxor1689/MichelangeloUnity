# MichelangeloUnity

## Spec

- Editor window for credentials
    - Log in, save cookies
    - Show remaining energy
- Game object with single `Michelangelo axiom`
    - Attributes
        - Name
        - With*(Split, Translate, Rotate, Paint, !Seed, !Attr)
        - Restrict*(Source.Mine/.This/.Team(teamNames...)/.Project)
    - Required name and scale
    - Parse JSON response and create mesh from it
- Material interpretation
    - Find a way to map between Michelangelo's and Unity's materials
- Allow users to add their own rules
    - Attach user rules to generation request
    - Probably use user selected editor for script writing (C# syntax highlight)
- Send multiple Michelangelo objects in one request for generation
    - Let user select all axioms that he wants to generate again
    - Internally assign ID's with FrontendID method
    - Find id's in response json
- Generation graph manipulation
    - Visualize it
    - Lock branches or recreate nodes

## JSON structure

- env
    - ml - physical materials
        - Glossy.ExtinctionCoefficient - (1 to 3) find mapping to metallic value
    - o - objects
        - g - primitive type (sphere, cube, cylinder, mesh)
        - m - material index
        - t - transfer
        - v - vertices for matrix
            - indexed - if indices are defined
            - indices
            - initialExtent - ?
        - n