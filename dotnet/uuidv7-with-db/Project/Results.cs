namespace Project;

public class Results : List<GeneratorRun>;

public record GeneratorRun(BaseGenerator Generator, Record[] Generated, List<SorterRun> SorterRuns);

public record SorterRun(ISorter Sorter, Record[] Records);