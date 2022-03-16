public class MinHeap
{
    public MinHeap(int[] input, int length)
    {

        this.Length = length;
        this.Array = input;
        BuildMinHeap();
    }

    public int[] Array { get; private set; }

    public int Length { get; private set; }

    private void BuildMinHeap()
    {
        for (int i = this.Length / 2; i > 0; i--)
        {
            MinHeapify(i);
        }

        return;
    }

    public void MinHeapify(int index)
    {
        var left = 2 * index;
        var right = 2 * index + 1;

        int Min = index;
        if (left <= this.Length && this.Array[left - 1] < this.Array[index - 1])
        {
            Min = left;
        }

        if (right <= this.Length && this.Array[right - 1] < this.Array[Min - 1])
        {
            Min = right;
        }

        if (Min != index)
        {
            int temp = this.Array[Min - 1];
            this.Array[Min - 1] = this.Array[index - 1];
            this.Array[index - 1] = temp;
            MinHeapify(Min);
        }

        return;
    }

    public int RemoveMinimum()
    {
        int Minimum = this.Array[0];

        this.Array[0] = this.Array[this.Length - 1];
        this.Length--;
        MinHeapify(1);
        return Minimum;
    }
}